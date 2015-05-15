namespace Ogama.MainWindow
{
    partial class MainForm
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
      this.mnuHelpContents = new System.Windows.Forms.ToolStripMenuItem();
      this.mnsMain = new System.Windows.Forms.MenuStrip();
      this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileNewExperiment = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileOpenExperiment = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileCloseExperiment = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
      this.mnuFileSaveExperiment = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileSaveExperimentAs = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuFilePrint = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFilePrintPreview = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuFileRecentFiles = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileClearRecentFileList = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
      this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuEditCopy = new System.Windows.Forms.ToolStripMenuItem();
      this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuEditSaveImage = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuViews = new System.Windows.Forms.ToolStripMenuItem();
      this.recordingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuViewsNewStimuliCreation = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuViewsNewRecording = new System.Windows.Forms.ToolStripMenuItem();
      this.analysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuViewsNewAOI = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuViewsNewAttentionMap = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuViewsNewSaliency = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuViewsNewReplay = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuViewsNewDatabase = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuViewsNewScanpaths = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuViewsNewFixations = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuViewsNewStatistics = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuViewsCloseChild = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuViewsStatusBar = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuViewsContextPanel = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuToolsImport = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuToolsRecalculateStimuliThumbs = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuToolsDatabaseConnection = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuToolsExperimentSettings = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuToolsOptions = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuWindow = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuWindowCascade = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuWindowTileVertical = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuWindowTileHorizontal = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuHelpSource = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuHelpCheckForUpdates = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuHelpIndex = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuHelpSearch = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
      this.ofdExperiment = new System.Windows.Forms.OpenFileDialog();
      this.sfdExperiment = new System.Windows.Forms.SaveFileDialog();
      this.stsMain = new System.Windows.Forms.StatusStrip();
      this.prbStatus = new System.Windows.Forms.ToolStripProgressBar();
      this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
      this.lblDataStates = new System.Windows.Forms.ToolStripStatusLabel();
      this.toolTip = new System.Windows.Forms.ToolTip(this.components);
      this.imlStimuli = new System.Windows.Forms.ImageList(this.components);
      this.imlTreeView = new System.Windows.Forms.ImageList(this.components);
      this.imlContextPanel = new System.Windows.Forms.ImageList(this.components);
      this.contextPanel = new Ogama.MainWindow.ContextPanel.ContextPanel();
      this.bgwLoad = new System.ComponentModel.BackgroundWorker();
      this.fbdExperiment = new System.Windows.Forms.FolderBrowserDialog();
      this.btnRPL = new System.Windows.Forms.ToolStripButton();
      this.btnATM = new System.Windows.Forms.ToolStripButton();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
      this.btnSCR = new System.Windows.Forms.ToolStripButton();
      this.btnREC = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
      this.btnSAL = new System.Windows.Forms.ToolStripButton();
      this.btnSCA = new System.Windows.Forms.ToolStripButton();
      this.btnDTB = new System.Windows.Forms.ToolStripButton();
      this.btnFIX = new System.Windows.Forms.ToolStripButton();
      this.btnSTA = new System.Windows.Forms.ToolStripButton();
      this.btnAOI = new System.Windows.Forms.ToolStripButton();
      this.mnsMain.SuspendLayout();
      this.stsMain.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // mnuHelpContents
      // 
      this.mnuHelpContents.Name = "mnuHelpContents";
      this.mnuHelpContents.Size = new System.Drawing.Size(207, 22);
      this.mnuHelpContents.Text = "&Contents";
      this.mnuHelpContents.Visible = false;
      this.mnuHelpContents.Click += new System.EventHandler(this.mnuHelpContents_Click);
      // 
      // mnsMain
      // 
      this.mnsMain.AllowMerge = false;
      this.mnsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.mnuViews,
            this.mnuTools,
            this.mnuWindow,
            this.mnuHelp});
      this.mnsMain.Location = new System.Drawing.Point(0, 0);
      this.mnsMain.MdiWindowListItem = this.mnuWindow;
      this.mnsMain.Name = "mnsMain";
      this.mnsMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
      this.mnsMain.Size = new System.Drawing.Size(564, 24);
      this.mnsMain.TabIndex = 1;
      this.mnsMain.Text = "menuStrip1";
      // 
      // mnuFile
      // 
      this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileNewExperiment,
            this.mnuFileOpenExperiment,
            this.mnuFileCloseExperiment,
            this.toolStripSeparator,
            this.mnuFileSaveExperiment,
            this.mnuFileSaveExperimentAs,
            this.toolStripSeparator1,
            this.mnuFilePrint,
            this.mnuFilePrintPreview,
            this.toolStripSeparator2,
            this.mnuFileExit,
            this.toolStripSeparator6,
            this.mnuFileRecentFiles,
            this.mnuFileClearRecentFileList});
      this.mnuFile.Name = "mnuFile";
      this.mnuFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F)));
      this.mnuFile.Size = new System.Drawing.Size(37, 20);
      this.mnuFile.Text = "&File";
      this.mnuFile.DropDownOpening += new System.EventHandler(this.mnuFile_DropDownOpening);
      // 
      // mnuFileNewExperiment
      // 
      this.mnuFileNewExperiment.Image = global::Ogama.Properties.Resources.OgamaNew;
      this.mnuFileNewExperiment.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.mnuFileNewExperiment.Name = "mnuFileNewExperiment";
      this.mnuFileNewExperiment.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
      this.mnuFileNewExperiment.Size = new System.Drawing.Size(208, 22);
      this.mnuFileNewExperiment.Text = "&New Experiment";
      this.mnuFileNewExperiment.Click += new System.EventHandler(this.mnuFileNewExperiment_Click);
      // 
      // mnuFileOpenExperiment
      // 
      this.mnuFileOpenExperiment.Image = global::Ogama.Properties.Resources.OpenSelectedItemHS;
      this.mnuFileOpenExperiment.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.mnuFileOpenExperiment.Name = "mnuFileOpenExperiment";
      this.mnuFileOpenExperiment.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
      this.mnuFileOpenExperiment.Size = new System.Drawing.Size(208, 22);
      this.mnuFileOpenExperiment.Text = "&Open Experiment";
      this.mnuFileOpenExperiment.Click += new System.EventHandler(this.mnuFileOpenExperiment_Click);
      // 
      // mnuFileCloseExperiment
      // 
      this.mnuFileCloseExperiment.Image = global::Ogama.Properties.Resources.DeleteHS;
      this.mnuFileCloseExperiment.Name = "mnuFileCloseExperiment";
      this.mnuFileCloseExperiment.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
      this.mnuFileCloseExperiment.Size = new System.Drawing.Size(208, 22);
      this.mnuFileCloseExperiment.Text = "&Close Experiment";
      this.mnuFileCloseExperiment.Click += new System.EventHandler(this.mnuFileCloseExperiment_Click);
      // 
      // toolStripSeparator
      // 
      this.toolStripSeparator.Name = "toolStripSeparator";
      this.toolStripSeparator.Size = new System.Drawing.Size(205, 6);
      // 
      // mnuFileSaveExperiment
      // 
      this.mnuFileSaveExperiment.Image = global::Ogama.Properties.Resources.saveHS;
      this.mnuFileSaveExperiment.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.mnuFileSaveExperiment.Name = "mnuFileSaveExperiment";
      this.mnuFileSaveExperiment.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
      this.mnuFileSaveExperiment.Size = new System.Drawing.Size(208, 22);
      this.mnuFileSaveExperiment.Text = "&Save Experiment";
      this.mnuFileSaveExperiment.Click += new System.EventHandler(this.mnuFileSaveExperiment_Click);
      // 
      // mnuFileSaveExperimentAs
      // 
      this.mnuFileSaveExperimentAs.Name = "mnuFileSaveExperimentAs";
      this.mnuFileSaveExperimentAs.Size = new System.Drawing.Size(208, 22);
      this.mnuFileSaveExperimentAs.Text = "Save Experiment &As";
      this.mnuFileSaveExperimentAs.Visible = false;
      this.mnuFileSaveExperimentAs.Click += new System.EventHandler(this.mnuFileSaveExperimentAs_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(205, 6);
      // 
      // mnuFilePrint
      // 
      this.mnuFilePrint.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.mnuFilePrint.Name = "mnuFilePrint";
      this.mnuFilePrint.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
      this.mnuFilePrint.Size = new System.Drawing.Size(208, 22);
      this.mnuFilePrint.Text = "&Print";
      this.mnuFilePrint.Visible = false;
      // 
      // mnuFilePrintPreview
      // 
      this.mnuFilePrintPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.mnuFilePrintPreview.Name = "mnuFilePrintPreview";
      this.mnuFilePrintPreview.Size = new System.Drawing.Size(208, 22);
      this.mnuFilePrintPreview.Text = "Print Pre&view";
      this.mnuFilePrintPreview.Visible = false;
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(205, 6);
      this.toolStripSeparator2.Visible = false;
      // 
      // mnuFileExit
      // 
      this.mnuFileExit.Name = "mnuFileExit";
      this.mnuFileExit.Size = new System.Drawing.Size(208, 22);
      this.mnuFileExit.Text = "E&xit";
      this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
      // 
      // toolStripSeparator6
      // 
      this.toolStripSeparator6.Name = "toolStripSeparator6";
      this.toolStripSeparator6.Size = new System.Drawing.Size(205, 6);
      // 
      // mnuFileRecentFiles
      // 
      this.mnuFileRecentFiles.Name = "mnuFileRecentFiles";
      this.mnuFileRecentFiles.Size = new System.Drawing.Size(208, 22);
      this.mnuFileRecentFiles.Text = "Recent Files ...";
      this.mnuFileRecentFiles.DropDownOpening += new System.EventHandler(this.mnuFileRecentFiles_DropDownOpening);
      // 
      // mnuFileClearRecentFileList
      // 
      this.mnuFileClearRecentFileList.Name = "mnuFileClearRecentFileList";
      this.mnuFileClearRecentFileList.Size = new System.Drawing.Size(208, 22);
      this.mnuFileClearRecentFileList.Text = "Clear Recent File List";
      this.mnuFileClearRecentFileList.Click += new System.EventHandler(this.mnuFileClearRecentFileList_Click);
      // 
      // mnuEdit
      // 
      this.mnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator3,
            this.cutToolStripMenuItem,
            this.mnuEditCopy,
            this.pasteToolStripMenuItem,
            this.mnuEditSaveImage,
            this.toolStripSeparator4,
            this.selectAllToolStripMenuItem});
      this.mnuEdit.Enabled = false;
      this.mnuEdit.Name = "mnuEdit";
      this.mnuEdit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.E)));
      this.mnuEdit.Size = new System.Drawing.Size(39, 20);
      this.mnuEdit.Text = "&Edit";
      // 
      // undoToolStripMenuItem
      // 
      this.undoToolStripMenuItem.Image = global::Ogama.Properties.Resources.Edit_UndoHS;
      this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
      this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
      this.undoToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
      this.undoToolStripMenuItem.Text = "&Undo";
      this.undoToolStripMenuItem.Visible = false;
      // 
      // redoToolStripMenuItem
      // 
      this.redoToolStripMenuItem.Image = global::Ogama.Properties.Resources.Edit_RedoHS;
      this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
      this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
      this.redoToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
      this.redoToolStripMenuItem.Text = "&Redo";
      this.redoToolStripMenuItem.Visible = false;
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(168, 6);
      this.toolStripSeparator3.Visible = false;
      // 
      // cutToolStripMenuItem
      // 
      this.cutToolStripMenuItem.Image = global::Ogama.Properties.Resources.CutHS;
      this.cutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
      this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
      this.cutToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
      this.cutToolStripMenuItem.Text = "Cu&t";
      // 
      // mnuEditCopy
      // 
      this.mnuEditCopy.Image = global::Ogama.Properties.Resources.CopyHS;
      this.mnuEditCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.mnuEditCopy.Name = "mnuEditCopy";
      this.mnuEditCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
      this.mnuEditCopy.Size = new System.Drawing.Size(171, 22);
      this.mnuEditCopy.Text = "&Copy";
      this.mnuEditCopy.Click += new System.EventHandler(this.mnuEditCopy_Click);
      // 
      // pasteToolStripMenuItem
      // 
      this.pasteToolStripMenuItem.Image = global::Ogama.Properties.Resources.PasteHS;
      this.pasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
      this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
      this.pasteToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
      this.pasteToolStripMenuItem.Text = "&Paste";
      // 
      // mnuEditSaveImage
      // 
      this.mnuEditSaveImage.Image = global::Ogama.Properties.Resources.saveHS;
      this.mnuEditSaveImage.Name = "mnuEditSaveImage";
      this.mnuEditSaveImage.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
      this.mnuEditSaveImage.Size = new System.Drawing.Size(171, 22);
      this.mnuEditSaveImage.Text = "&Save Image";
      this.mnuEditSaveImage.Click += new System.EventHandler(this.mnuEditSaveImage_Click);
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(168, 6);
      this.toolStripSeparator4.Visible = false;
      // 
      // selectAllToolStripMenuItem
      // 
      this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
      this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
      this.selectAllToolStripMenuItem.Text = "Select &All";
      this.selectAllToolStripMenuItem.Visible = false;
      // 
      // mnuViews
      // 
      this.mnuViews.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.recordingToolStripMenuItem,
            this.analysisToolStripMenuItem,
            this.toolStripMenuItem1,
            this.mnuViewsCloseChild,
            this.toolStripSeparator7,
            this.mnuViewsStatusBar,
            this.mnuViewsContextPanel});
      this.mnuViews.Enabled = false;
      this.mnuViews.Name = "mnuViews";
      this.mnuViews.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.V)));
      this.mnuViews.Size = new System.Drawing.Size(49, 20);
      this.mnuViews.Text = "&Views";
      this.mnuViews.DropDownOpening += new System.EventHandler(this.mnuViews_DropDownOpening);
      // 
      // recordingToolStripMenuItem
      // 
      this.recordingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewsNewStimuliCreation,
            this.mnuViewsNewRecording});
      this.recordingToolStripMenuItem.Image = global::Ogama.Properties.Resources.Design;
      this.recordingToolStripMenuItem.Name = "recordingToolStripMenuItem";
      this.recordingToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
      this.recordingToolStripMenuItem.Text = "Design and Record";
      // 
      // mnuViewsNewStimuliCreation
      // 
      this.mnuViewsNewStimuliCreation.Image = global::Ogama.Properties.Resources.Design;
      this.mnuViewsNewStimuliCreation.Name = "mnuViewsNewStimuliCreation";
      this.mnuViewsNewStimuliCreation.Size = new System.Drawing.Size(214, 22);
      this.mnuViewsNewStimuliCreation.Text = "New Study Design module";
      this.mnuViewsNewStimuliCreation.Click += new System.EventHandler(this.mnuViewsNewStimuliCreation_Click);
      // 
      // mnuViewsNewRecording
      // 
      this.mnuViewsNewRecording.Image = global::Ogama.Properties.Resources.Record;
      this.mnuViewsNewRecording.Name = "mnuViewsNewRecording";
      this.mnuViewsNewRecording.Size = new System.Drawing.Size(214, 22);
      this.mnuViewsNewRecording.Text = "New Recording module";
      this.mnuViewsNewRecording.Click += new System.EventHandler(this.mnuViewsNewRecording_Click);
      // 
      // analysisToolStripMenuItem
      // 
      this.analysisToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewsNewAOI,
            this.mnuViewsNewAttentionMap,
            this.mnuViewsNewSaliency,
            this.mnuViewsNewReplay,
            this.mnuViewsNewDatabase,
            this.mnuViewsNewScanpaths,
            this.mnuViewsNewFixations,
            this.mnuViewsNewStatistics});
      this.analysisToolStripMenuItem.Image = global::Ogama.Properties.Resources.Run;
      this.analysisToolStripMenuItem.Name = "analysisToolStripMenuItem";
      this.analysisToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
      this.analysisToolStripMenuItem.Text = "Analyse";
      // 
      // mnuViewsNewAOI
      // 
      this.mnuViewsNewAOI.Image = global::Ogama.Properties.Resources.AOILogo;
      this.mnuViewsNewAOI.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.mnuViewsNewAOI.Name = "mnuViewsNewAOI";
      this.mnuViewsNewAOI.Size = new System.Drawing.Size(222, 22);
      this.mnuViewsNewAOI.Text = "New &AOI Module";
      this.mnuViewsNewAOI.Click += new System.EventHandler(this.mnuViewsNewAOI_Click);
      // 
      // mnuViewsNewAttentionMap
      // 
      this.mnuViewsNewAttentionMap.Image = global::Ogama.Properties.Resources.AttentionMapLogo;
      this.mnuViewsNewAttentionMap.Name = "mnuViewsNewAttentionMap";
      this.mnuViewsNewAttentionMap.Size = new System.Drawing.Size(222, 22);
      this.mnuViewsNewAttentionMap.Text = "New A&ttention Map Module";
      this.mnuViewsNewAttentionMap.Click += new System.EventHandler(this.mnuViewsNewAttentionMap_Click);
      // 
      // mnuViewsNewSaliency
      // 
      this.mnuViewsNewSaliency.Image = global::Ogama.Properties.Resources.Saliency;
      this.mnuViewsNewSaliency.Name = "mnuViewsNewSaliency";
      this.mnuViewsNewSaliency.Size = new System.Drawing.Size(222, 22);
      this.mnuViewsNewSaliency.Text = "New Salienc&y Module";
      this.mnuViewsNewSaliency.Click += new System.EventHandler(this.mnuViewsNewSaliency_Click);
      // 
      // mnuViewsNewReplay
      // 
      this.mnuViewsNewReplay.Image = global::Ogama.Properties.Resources.RPL;
      this.mnuViewsNewReplay.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.mnuViewsNewReplay.Name = "mnuViewsNewReplay";
      this.mnuViewsNewReplay.Size = new System.Drawing.Size(222, 22);
      this.mnuViewsNewReplay.Text = "New &Replay Module";
      this.mnuViewsNewReplay.Click += new System.EventHandler(this.mnuViewsNewReplay_Click);
      // 
      // mnuViewsNewDatabase
      // 
      this.mnuViewsNewDatabase.Image = global::Ogama.Properties.Resources.DatabaseLogo;
      this.mnuViewsNewDatabase.Name = "mnuViewsNewDatabase";
      this.mnuViewsNewDatabase.Size = new System.Drawing.Size(222, 22);
      this.mnuViewsNewDatabase.Text = "New &Database Module";
      this.mnuViewsNewDatabase.Click += new System.EventHandler(this.mnuViewsNewDatabase_Click);
      // 
      // mnuViewsNewScanpaths
      // 
      this.mnuViewsNewScanpaths.Image = global::Ogama.Properties.Resources.Scanpath;
      this.mnuViewsNewScanpaths.Name = "mnuViewsNewScanpaths";
      this.mnuViewsNewScanpaths.Size = new System.Drawing.Size(222, 22);
      this.mnuViewsNewScanpaths.Text = "New Scan&path Module";
      this.mnuViewsNewScanpaths.Click += new System.EventHandler(this.mnuViewsNewScanpaths_Click);
      // 
      // mnuViewsNewFixations
      // 
      this.mnuViewsNewFixations.Image = global::Ogama.Properties.Resources.FixationsLogo;
      this.mnuViewsNewFixations.Name = "mnuViewsNewFixations";
      this.mnuViewsNewFixations.Size = new System.Drawing.Size(222, 22);
      this.mnuViewsNewFixations.Text = "New &Fixations Module";
      this.mnuViewsNewFixations.Click += new System.EventHandler(this.mnuViewsNewFixations_Click);
      // 
      // mnuViewsNewStatistics
      // 
      this.mnuViewsNewStatistics.Image = global::Ogama.Properties.Resources.StatisticsLogo;
      this.mnuViewsNewStatistics.Name = "mnuViewsNewStatistics";
      this.mnuViewsNewStatistics.Size = new System.Drawing.Size(222, 22);
      this.mnuViewsNewStatistics.Text = "New &Statistics Module";
      this.mnuViewsNewStatistics.Click += new System.EventHandler(this.mnuViewsNewStatistics_Click);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(180, 6);
      // 
      // mnuViewsCloseChild
      // 
      this.mnuViewsCloseChild.Image = global::Ogama.Properties.Resources.DeleteHS;
      this.mnuViewsCloseChild.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.mnuViewsCloseChild.Name = "mnuViewsCloseChild";
      this.mnuViewsCloseChild.Size = new System.Drawing.Size(183, 22);
      this.mnuViewsCloseChild.Text = "&Close Active Module";
      this.mnuViewsCloseChild.Click += new System.EventHandler(this.mnuViewsCloseChild_Click);
      // 
      // toolStripSeparator7
      // 
      this.toolStripSeparator7.Name = "toolStripSeparator7";
      this.toolStripSeparator7.Size = new System.Drawing.Size(180, 6);
      // 
      // mnuViewsStatusBar
      // 
      this.mnuViewsStatusBar.Checked = true;
      this.mnuViewsStatusBar.CheckOnClick = true;
      this.mnuViewsStatusBar.CheckState = System.Windows.Forms.CheckState.Checked;
      this.mnuViewsStatusBar.Name = "mnuViewsStatusBar";
      this.mnuViewsStatusBar.Size = new System.Drawing.Size(183, 22);
      this.mnuViewsStatusBar.Text = "Statusbar";
      this.mnuViewsStatusBar.Click += new System.EventHandler(this.mnuViewsStatusBar_Click);
      // 
      // mnuViewsContextPanel
      // 
      this.mnuViewsContextPanel.Checked = true;
      this.mnuViewsContextPanel.CheckOnClick = true;
      this.mnuViewsContextPanel.CheckState = System.Windows.Forms.CheckState.Checked;
      this.mnuViewsContextPanel.Name = "mnuViewsContextPanel";
      this.mnuViewsContextPanel.Size = new System.Drawing.Size(183, 22);
      this.mnuViewsContextPanel.Text = "Context panel";
      this.mnuViewsContextPanel.Click += new System.EventHandler(this.mnuViewsContextPanel_Click);
      // 
      // mnuTools
      // 
      this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuToolsImport,
            this.mnuToolsRecalculateStimuliThumbs,
            this.toolStripSeparator8,
            this.mnuToolsDatabaseConnection,
            this.toolStripSeparator10,
            this.mnuToolsExperimentSettings,
            this.mnuToolsOptions});
      this.mnuTools.Enabled = false;
      this.mnuTools.Name = "mnuTools";
      this.mnuTools.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.T)));
      this.mnuTools.Size = new System.Drawing.Size(48, 20);
      this.mnuTools.Text = "&Tools";
      // 
      // mnuToolsImport
      // 
      this.mnuToolsImport.Image = global::Ogama.Properties.Resources.MagicWand;
      this.mnuToolsImport.Name = "mnuToolsImport";
      this.mnuToolsImport.Size = new System.Drawing.Size(248, 22);
      this.mnuToolsImport.Text = "Import gaze or mouse samples ...";
      this.mnuToolsImport.Click += new System.EventHandler(this.mnuToolsImport_Click);
      // 
      // mnuToolsRecalculateStimuliThumbs
      // 
      this.mnuToolsRecalculateStimuliThumbs.Image = global::Ogama.Properties.Resources.rc_tif;
      this.mnuToolsRecalculateStimuliThumbs.Name = "mnuToolsRecalculateStimuliThumbs";
      this.mnuToolsRecalculateStimuliThumbs.Size = new System.Drawing.Size(248, 22);
      this.mnuToolsRecalculateStimuliThumbs.Text = "Recalculate Stimuli Thumbs";
      this.mnuToolsRecalculateStimuliThumbs.Click += new System.EventHandler(this.mnuToolsRecalculateStimuliThumbs_Click);
      // 
      // toolStripSeparator8
      // 
      this.toolStripSeparator8.Name = "toolStripSeparator8";
      this.toolStripSeparator8.Size = new System.Drawing.Size(245, 6);
      // 
      // mnuToolsDatabaseConnection
      // 
      this.mnuToolsDatabaseConnection.Image = global::Ogama.Properties.Resources.SQLDatabase;
      this.mnuToolsDatabaseConnection.Name = "mnuToolsDatabaseConnection";
      this.mnuToolsDatabaseConnection.Size = new System.Drawing.Size(248, 22);
      this.mnuToolsDatabaseConnection.Text = "Database Connection ...";
      this.mnuToolsDatabaseConnection.Visible = false;
      this.mnuToolsDatabaseConnection.Click += new System.EventHandler(this.mnuToolsDatabaseConnection_Click);
      // 
      // toolStripSeparator10
      // 
      this.toolStripSeparator10.Name = "toolStripSeparator10";
      this.toolStripSeparator10.Size = new System.Drawing.Size(245, 6);
      this.toolStripSeparator10.Visible = false;
      // 
      // mnuToolsExperimentSettings
      // 
      this.mnuToolsExperimentSettings.Image = global::Ogama.Properties.Resources.otheroptions;
      this.mnuToolsExperimentSettings.Name = "mnuToolsExperimentSettings";
      this.mnuToolsExperimentSettings.Size = new System.Drawing.Size(248, 22);
      this.mnuToolsExperimentSettings.Text = "Experiment Settings...";
      this.mnuToolsExperimentSettings.Click += new System.EventHandler(this.mnuToolsExperimentSettings_Click);
      // 
      // mnuToolsOptions
      // 
      this.mnuToolsOptions.Image = global::Ogama.Properties.Resources.CheckBoxHS;
      this.mnuToolsOptions.Name = "mnuToolsOptions";
      this.mnuToolsOptions.Size = new System.Drawing.Size(248, 22);
      this.mnuToolsOptions.Text = "&Options ...";
      this.mnuToolsOptions.Click += new System.EventHandler(this.mnuToolsOptions_Click);
      // 
      // mnuWindow
      // 
      this.mnuWindow.Checked = true;
      this.mnuWindow.CheckState = System.Windows.Forms.CheckState.Checked;
      this.mnuWindow.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuWindowCascade,
            this.mnuWindowTileVertical,
            this.mnuWindowTileHorizontal});
      this.mnuWindow.Name = "mnuWindow";
      this.mnuWindow.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.W)));
      this.mnuWindow.Size = new System.Drawing.Size(63, 20);
      this.mnuWindow.Text = "&Window";
      this.mnuWindow.DropDownOpening += new System.EventHandler(this.mnuWindow_DropDownOpening);
      // 
      // mnuWindowCascade
      // 
      this.mnuWindowCascade.Image = global::Ogama.Properties.Resources.CascadeWindowsHS;
      this.mnuWindowCascade.Name = "mnuWindowCascade";
      this.mnuWindowCascade.Size = new System.Drawing.Size(151, 22);
      this.mnuWindowCascade.Text = "&Cascade";
      this.mnuWindowCascade.Click += new System.EventHandler(this.mnuWindowCascade_Click);
      // 
      // mnuWindowTileVertical
      // 
      this.mnuWindowTileVertical.Name = "mnuWindowTileVertical";
      this.mnuWindowTileVertical.Size = new System.Drawing.Size(151, 22);
      this.mnuWindowTileVertical.Text = "Tile &Vertical";
      this.mnuWindowTileVertical.Click += new System.EventHandler(this.mnuWindowTileVertical_Click);
      // 
      // mnuWindowTileHorizontal
      // 
      this.mnuWindowTileHorizontal.Image = global::Ogama.Properties.Resources.TileWindowsHorizontallyHS;
      this.mnuWindowTileHorizontal.Name = "mnuWindowTileHorizontal";
      this.mnuWindowTileHorizontal.Size = new System.Drawing.Size(151, 22);
      this.mnuWindowTileHorizontal.Text = "Tile &Horizontal";
      this.mnuWindowTileHorizontal.Click += new System.EventHandler(this.mnuWindowTileHorizontal_Click);
      // 
      // mnuHelp
      // 
      this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelpSource,
            this.mnuHelpCheckForUpdates,
            this.mnuHelpContents,
            this.mnuHelpIndex,
            this.mnuHelpSearch,
            this.toolStripSeparator5,
            this.mnuHelpAbout});
      this.mnuHelp.Name = "mnuHelp";
      this.mnuHelp.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.H)));
      this.mnuHelp.Size = new System.Drawing.Size(44, 20);
      this.mnuHelp.Text = "&Help";
      this.mnuHelp.DropDownOpening += new System.EventHandler(this.mnuHelp_DropDownOpening);
      // 
      // mnuHelpSource
      // 
      this.mnuHelpSource.Image = global::Ogama.Properties.Resources.helpdoc;
      this.mnuHelpSource.Name = "mnuHelpSource";
      this.mnuHelpSource.Size = new System.Drawing.Size(207, 22);
      this.mnuHelpSource.Text = "&Source documentation ...";
      this.mnuHelpSource.Click += new System.EventHandler(this.mnuHelpSource_Click);
      // 
      // mnuHelpCheckForUpdates
      // 
      this.mnuHelpCheckForUpdates.Image = global::Ogama.Properties.Resources.SearchWebHS;
      this.mnuHelpCheckForUpdates.Name = "mnuHelpCheckForUpdates";
      this.mnuHelpCheckForUpdates.Size = new System.Drawing.Size(207, 22);
      this.mnuHelpCheckForUpdates.Text = "Check for Updates ...";
      this.mnuHelpCheckForUpdates.Click += new System.EventHandler(this.mnuHelpCheckForUpdates_Click);
      // 
      // mnuHelpIndex
      // 
      this.mnuHelpIndex.Name = "mnuHelpIndex";
      this.mnuHelpIndex.Size = new System.Drawing.Size(207, 22);
      this.mnuHelpIndex.Text = "&Index";
      this.mnuHelpIndex.Visible = false;
      // 
      // mnuHelpSearch
      // 
      this.mnuHelpSearch.Name = "mnuHelpSearch";
      this.mnuHelpSearch.Size = new System.Drawing.Size(207, 22);
      this.mnuHelpSearch.Text = "&Search";
      this.mnuHelpSearch.Visible = false;
      // 
      // toolStripSeparator5
      // 
      this.toolStripSeparator5.Name = "toolStripSeparator5";
      this.toolStripSeparator5.Size = new System.Drawing.Size(204, 6);
      // 
      // mnuHelpAbout
      // 
      this.mnuHelpAbout.Image = ((System.Drawing.Image)(resources.GetObject("mnuHelpAbout.Image")));
      this.mnuHelpAbout.Name = "mnuHelpAbout";
      this.mnuHelpAbout.Size = new System.Drawing.Size(207, 22);
      this.mnuHelpAbout.Text = "&About...";
      this.mnuHelpAbout.Click += new System.EventHandler(this.mnuHelpAbout_Click);
      // 
      // ofdExperiment
      // 
      this.ofdExperiment.DefaultExt = "oga";
      this.ofdExperiment.FileName = "*.oga";
      this.ofdExperiment.Filter = "Ogama files (*.oga)|*.oga|All files|*.*";
      this.ofdExperiment.Title = "Select experiment to load ...";
      // 
      // sfdExperiment
      // 
      this.sfdExperiment.DefaultExt = "oga";
      this.sfdExperiment.FileName = "Experiment1.oga";
      this.sfdExperiment.Filter = "Ogama files (*.oga)|*.oga|All files|*.*";
      this.sfdExperiment.Title = "Choose Destination and File to Save Experiment ...";
      // 
      // stsMain
      // 
      this.stsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.prbStatus,
            this.lblStatus,
            this.lblDataStates});
      this.stsMain.Location = new System.Drawing.Point(0, 391);
      this.stsMain.Name = "stsMain";
      this.stsMain.Size = new System.Drawing.Size(564, 22);
      this.stsMain.SizingGrip = false;
      this.stsMain.TabIndex = 3;
      this.stsMain.Text = "statusStrip1";
      // 
      // prbStatus
      // 
      this.prbStatus.Name = "prbStatus";
      this.prbStatus.Size = new System.Drawing.Size(100, 16);
      this.prbStatus.Step = 20;
      this.prbStatus.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
      // 
      // lblStatus
      // 
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new System.Drawing.Size(39, 17);
      this.lblStatus.Text = "Ready";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lblDataStates
      // 
      this.lblDataStates.Name = "lblDataStates";
      this.lblDataStates.Size = new System.Drawing.Size(408, 17);
      this.lblDataStates.Spring = true;
      this.lblDataStates.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // imlStimuli
      // 
      this.imlStimuli.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
      this.imlStimuli.ImageSize = new System.Drawing.Size(64, 48);
      this.imlStimuli.TransparentColor = System.Drawing.Color.Transparent;
      // 
      // imlTreeView
      // 
      this.imlTreeView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlTreeView.ImageStream")));
      this.imlTreeView.TransparentColor = System.Drawing.Color.Transparent;
      this.imlTreeView.Images.SetKeyName(0, "folder");
      this.imlTreeView.Images.SetKeyName(1, "bitmap");
      this.imlTreeView.Images.SetKeyName(2, "User");
      // 
      // imlContextPanel
      // 
      this.imlContextPanel.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlContextPanel.ImageStream")));
      this.imlContextPanel.TransparentColor = System.Drawing.Color.Transparent;
      this.imlContextPanel.Images.SetKeyName(0, "StimuliList");
      this.imlContextPanel.Images.SetKeyName(1, "StimuliPictures");
      this.imlContextPanel.Images.SetKeyName(2, "Help");
      this.imlContextPanel.Images.SetKeyName(3, "User");
      // 
      // contextPanel
      // 
      this.contextPanel.Dock = System.Windows.Forms.DockStyle.Right;
      this.contextPanel.HelpTabCaption = "Module name";
      this.contextPanel.IsUsercamVisible = true;
      this.contextPanel.Location = new System.Drawing.Point(564, 0);
      this.contextPanel.Name = "contextPanel";
      this.contextPanel.Size = new System.Drawing.Size(200, 413);
      this.contextPanel.TabIndex = 7;
      // 
      // bgwLoad
      // 
      this.bgwLoad.WorkerSupportsCancellation = true;
      this.bgwLoad.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwLoad_DoWork);
      // 
      // fbdExperiment
      // 
      this.fbdExperiment.Description = "Select or create the folder where the new experiment should be located.";
      // 
      // btnRPL
      // 
      this.btnRPL.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnRPL.Image = global::Ogama.Properties.Resources.RPL;
      this.btnRPL.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnRPL.Name = "btnRPL";
      this.btnRPL.Size = new System.Drawing.Size(23, 22);
      this.btnRPL.Text = "Shows replay module";
      this.btnRPL.Click += new System.EventHandler(this.btnRPL_Click);
      // 
      // btnATM
      // 
      this.btnATM.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnATM.Image = global::Ogama.Properties.Resources.AttentionMapLogo;
      this.btnATM.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnATM.Name = "btnATM";
      this.btnATM.Size = new System.Drawing.Size(23, 22);
      this.btnATM.Text = "Shows attention map module";
      this.btnATM.Click += new System.EventHandler(this.btnATM_Click);
      // 
      // toolStrip1
      // 
      this.toolStrip1.AllowMerge = false;
      this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.btnSCR,
            this.btnREC,
            this.toolStripSeparator9,
            this.toolStripLabel1,
            this.btnRPL,
            this.btnATM,
            this.btnSAL,
            this.btnSCA,
            this.btnDTB,
            this.btnFIX,
            this.btnSTA,
            this.btnAOI});
      this.toolStrip1.Location = new System.Drawing.Point(0, 24);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.toolStrip1.Size = new System.Drawing.Size(564, 25);
      this.toolStrip1.TabIndex = 9;
      // 
      // toolStripLabel2
      // 
      this.toolStripLabel2.Enabled = false;
      this.toolStripLabel2.Name = "toolStripLabel2";
      this.toolStripLabel2.Size = new System.Drawing.Size(155, 22);
      this.toolStripLabel2.Text = "Design and Record Modules";
      // 
      // btnSCR
      // 
      this.btnSCR.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnSCR.Image = global::Ogama.Properties.Resources.Design;
      this.btnSCR.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnSCR.Name = "btnSCR";
      this.btnSCR.Size = new System.Drawing.Size(23, 22);
      this.btnSCR.Text = "Shows slideshow design module";
      this.btnSCR.Click += new System.EventHandler(this.btnSCR_Click);
      // 
      // btnREC
      // 
      this.btnREC.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnREC.Image = global::Ogama.Properties.Resources.Record;
      this.btnREC.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnREC.Name = "btnREC";
      this.btnREC.Size = new System.Drawing.Size(23, 22);
      this.btnREC.Text = "Shows recording module";
      this.btnREC.Click += new System.EventHandler(this.btnREC_Click);
      // 
      // toolStripSeparator9
      // 
      this.toolStripSeparator9.Name = "toolStripSeparator9";
      this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
      // 
      // toolStripLabel1
      // 
      this.toolStripLabel1.Enabled = false;
      this.toolStripLabel1.Name = "toolStripLabel1";
      this.toolStripLabel1.Size = new System.Drawing.Size(111, 22);
      this.toolStripLabel1.Text = "    Analysis Modules";
      // 
      // btnSAL
      // 
      this.btnSAL.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnSAL.Image = global::Ogama.Properties.Resources.Saliency;
      this.btnSAL.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnSAL.Name = "btnSAL";
      this.btnSAL.Size = new System.Drawing.Size(23, 22);
      this.btnSAL.Text = "Shows saliency module";
      this.btnSAL.Click += new System.EventHandler(this.btnSAL_Click);
      // 
      // btnSCA
      // 
      this.btnSCA.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnSCA.Image = global::Ogama.Properties.Resources.Scanpath;
      this.btnSCA.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnSCA.Name = "btnSCA";
      this.btnSCA.Size = new System.Drawing.Size(23, 22);
      this.btnSCA.Text = "Shows scanpath module";
      this.btnSCA.Click += new System.EventHandler(this.btnSCA_Click);
      // 
      // btnDTB
      // 
      this.btnDTB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnDTB.Image = global::Ogama.Properties.Resources.DatabaseLogo;
      this.btnDTB.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnDTB.Name = "btnDTB";
      this.btnDTB.Size = new System.Drawing.Size(23, 22);
      this.btnDTB.Text = "Shows database module";
      this.btnDTB.Click += new System.EventHandler(this.btnDTB_Click);
      // 
      // btnFIX
      // 
      this.btnFIX.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnFIX.Image = global::Ogama.Properties.Resources.FixationsLogo;
      this.btnFIX.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnFIX.Name = "btnFIX";
      this.btnFIX.Size = new System.Drawing.Size(23, 22);
      this.btnFIX.Text = "Shows fixations module";
      this.btnFIX.Click += new System.EventHandler(this.btnFIX_Click);
      // 
      // btnSTA
      // 
      this.btnSTA.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnSTA.Image = global::Ogama.Properties.Resources.StatisticsLogo;
      this.btnSTA.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnSTA.Name = "btnSTA";
      this.btnSTA.Size = new System.Drawing.Size(23, 22);
      this.btnSTA.Text = "Shows statistics module";
      this.btnSTA.Click += new System.EventHandler(this.btnSTA_Click);
      // 
      // btnAOI
      // 
      this.btnAOI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnAOI.Image = global::Ogama.Properties.Resources.AOILogo;
      this.btnAOI.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnAOI.Name = "btnAOI";
      this.btnAOI.Size = new System.Drawing.Size(23, 22);
      this.btnAOI.Text = "Shows areas of interest module";
      this.btnAOI.Click += new System.EventHandler(this.btnAOI_Click);
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(764, 413);
      this.Controls.Add(this.toolStrip1);
      this.Controls.Add(this.mnsMain);
      this.Controls.Add(this.stsMain);
      this.Controls.Add(this.contextPanel);
      this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Ogama.Properties.Settings.Default, "MainWindowLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.IsMdiContainer = true;
      this.Location = global::Ogama.Properties.Settings.Default.MainWindowLocation;
      this.MainMenuStrip = this.mnsMain;
      this.Name = "MainForm";
      this.Text = "Ogama";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
      this.Load += new System.EventHandler(this.MainForm_Load);
      this.mnsMain.ResumeLayout(false);
      this.mnsMain.PerformLayout();
      this.stsMain.ResumeLayout(false);
      this.stsMain.PerformLayout();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnsMain;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFileOpenExperiment;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem mnuFileSaveExperiment;
        private System.Windows.Forms.ToolStripMenuItem mnuFileSaveExperimentAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuFilePrint;
        private System.Windows.Forms.ToolStripMenuItem mnuFilePrintPreview;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuEditCopy;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem mnuTools;
        private System.Windows.Forms.ToolStripMenuItem mnuToolsOptions;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpSource;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpIndex;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpSearch;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpAbout;
        private System.Windows.Forms.ToolStripMenuItem mnuWindow;
        private System.Windows.Forms.ToolStripMenuItem mnuWindowCascade;
        private System.Windows.Forms.ToolStripMenuItem mnuWindowTileVertical;
        private System.Windows.Forms.ToolStripMenuItem mnuWindowTileHorizontal;
        private System.Windows.Forms.ToolStripMenuItem mnuToolsExperimentSettings;
      private System.Windows.Forms.ToolStripMenuItem mnuViews;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuViewsCloseChild;
        private System.Windows.Forms.ToolStripMenuItem mnuFileCloseExperiment;
        private System.Windows.Forms.ToolStripMenuItem mnuFileNewExperiment;
        private System.Windows.Forms.OpenFileDialog ofdExperiment;
      private System.Windows.Forms.SaveFileDialog sfdExperiment;
      private System.Windows.Forms.StatusStrip stsMain;
      private System.Windows.Forms.ToolStripMenuItem mnuFileRecentFiles;
      private System.Windows.Forms.ToolStripMenuItem mnuFileClearRecentFileList;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
      private System.Windows.Forms.ToolTip toolTip;
      private System.Windows.Forms.ImageList imlStimuli;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
      private System.Windows.Forms.ToolStripMenuItem mnuViewsStatusBar;
      private System.Windows.Forms.ToolStripMenuItem mnuViewsContextPanel;
      private System.Windows.Forms.ToolStripMenuItem mnuToolsRecalculateStimuliThumbs;
      private System.Windows.Forms.ToolStripMenuItem mnuEditSaveImage;
      private System.Windows.Forms.ImageList imlContextPanel;
      private System.Windows.Forms.ImageList imlTreeView;
      private ContextPanel.ContextPanel contextPanel;
      private System.ComponentModel.BackgroundWorker bgwLoad;
      private System.Windows.Forms.ToolStripStatusLabel lblStatus;
      private System.Windows.Forms.ToolStripProgressBar prbStatus;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
      private System.Windows.Forms.ToolStripStatusLabel lblDataStates;
      private System.Windows.Forms.ToolStripMenuItem mnuToolsImport;
      private System.Windows.Forms.ToolStripMenuItem analysisToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem mnuViewsNewAOI;
      private System.Windows.Forms.ToolStripMenuItem mnuViewsNewAttentionMap;
      private System.Windows.Forms.ToolStripMenuItem mnuViewsNewReplay;
      private System.Windows.Forms.ToolStripMenuItem mnuViewsNewDatabase;
      private System.Windows.Forms.ToolStripMenuItem mnuViewsNewFixations;
      private System.Windows.Forms.ToolStripMenuItem mnuViewsNewStatistics;
      private System.Windows.Forms.ToolStripMenuItem recordingToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem mnuViewsNewStimuliCreation;
      private System.Windows.Forms.ToolStripMenuItem mnuViewsNewRecording;
      private System.Windows.Forms.FolderBrowserDialog fbdExperiment;
      private System.Windows.Forms.ToolStripMenuItem mnuHelpContents;
      private System.Windows.Forms.ToolStripButton btnRPL;
      private System.Windows.Forms.ToolStripButton btnATM;
      private System.Windows.Forms.ToolStrip toolStrip1;
      private System.Windows.Forms.ToolStripButton btnDTB;
      private System.Windows.Forms.ToolStripButton btnFIX;
      private System.Windows.Forms.ToolStripButton btnSTA;
      private System.Windows.Forms.ToolStripButton btnAOI;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
      private System.Windows.Forms.ToolStripButton btnSCR;
      private System.Windows.Forms.ToolStripButton btnREC;
      private System.Windows.Forms.ToolStripLabel toolStripLabel1;
      private System.Windows.Forms.ToolStripLabel toolStripLabel2;
      private System.Windows.Forms.ToolStripMenuItem mnuViewsNewSaliency;
      private System.Windows.Forms.ToolStripButton btnSAL;
      private System.Windows.Forms.ToolStripMenuItem mnuToolsDatabaseConnection;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
      private System.Windows.Forms.ToolStripMenuItem mnuHelpCheckForUpdates;
      private System.Windows.Forms.ToolStripButton btnSCA;
      private System.Windows.Forms.ToolStripMenuItem mnuViewsNewScanpaths;
    }
}

