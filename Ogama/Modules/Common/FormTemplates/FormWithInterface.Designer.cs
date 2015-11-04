namespace Ogama.Modules.Common.FormTemplates
{
  using System.Windows.Forms;

  using Ogama.DataSet;

  partial class FormWithInterface
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
      this.CustomDispose();
      if (disposing && (this.components != null))
      {
        this.components.Dispose();
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
      this.ogamaDataSet = new SQLiteOgamaDataSet();
      this.bsoSubjects = new System.Windows.Forms.BindingSource(this.components);
      this.bsoFKSubjectsTrials = new System.Windows.Forms.BindingSource(this.components);
      this.bsoFKTrialsEvents = new System.Windows.Forms.BindingSource(this.components);
      this.bsoTrialsAOIs = new System.Windows.Forms.BindingSource(this.components);
      this.bsoTrialsGazeFixations = new System.Windows.Forms.BindingSource(this.components);
      this.bsoTrialsMouseFixations = new System.Windows.Forms.BindingSource(this.components);
      this.bsoShapeGroups = new System.Windows.Forms.BindingSource(this.components);
      this.bsoFKSubjectsSubjectParameters = new System.Windows.Forms.BindingSource(this.components);
      this.bsoSubjectParameters = new System.Windows.Forms.BindingSource(this.components);
      this.bsoTrialEvents = new System.Windows.Forms.BindingSource(this.components);
      this.bsoGazeFixations = new System.Windows.Forms.BindingSource(this.components);
      this.bsoMouseFixations = new System.Windows.Forms.BindingSource(this.components);
      this.bsoAOIs = new System.Windows.Forms.BindingSource(this.components);
      this.bsoTrials = new System.Windows.Forms.BindingSource(this.components);
      this.bsoParams = new System.Windows.Forms.BindingSource(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.ogamaDataSet)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoSubjects)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoFKSubjectsTrials)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoFKTrialsEvents)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoTrialsAOIs)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoTrialsGazeFixations)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoTrialsMouseFixations)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoShapeGroups)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoFKSubjectsSubjectParameters)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoSubjectParameters)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoTrialEvents)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoGazeFixations)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoMouseFixations)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoAOIs)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoTrials)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoParams)).BeginInit();
      this.SuspendLayout();
      // 
      // ogamaDataSet
      // 
      this.ogamaDataSet.DataSetName = "OgamaDataSet";
      this.ogamaDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
      // 
      // bsoSubjects
      // 
      this.bsoSubjects.DataMember = "Subjects";
      this.bsoSubjects.DataSource = this.ogamaDataSet;
      this.bsoSubjects.Sort = "SubjectName ASC";
      // 
      // bsoFKSubjectsTrials
      // 
      this.bsoFKSubjectsTrials.DataMember = "FK_Subjects_Trials";
      this.bsoFKSubjectsTrials.DataSource = this.bsoSubjects;
      // 
      // bsoFKTrialsEvents
      // 
      this.bsoFKTrialsEvents.DataMember = "FK_Trials_Events";
      this.bsoFKTrialsEvents.DataSource = this.bsoFKSubjectsTrials;
      // 
      // bsoTrialsAOIs
      // 
      this.bsoTrialsAOIs.DataMember = "Trials_AOIs";
      this.bsoTrialsAOIs.DataSource = this.bsoFKSubjectsTrials;
      // 
      // bsoTrialsGazeFixations
      // 
      this.bsoTrialsGazeFixations.DataMember = "Trials_GazeFixations";
      this.bsoTrialsGazeFixations.DataSource = this.bsoFKSubjectsTrials;
      // 
      // bsoTrialsMouseFixations
      // 
      this.bsoTrialsMouseFixations.DataMember = "Trials_MouseFixations";
      this.bsoTrialsMouseFixations.DataSource = this.bsoFKSubjectsTrials;
      // 
      // bsoShapeGroups
      // 
      this.bsoShapeGroups.DataMember = "ShapeGroups";
      this.bsoShapeGroups.DataSource = this.ogamaDataSet;
      this.bsoShapeGroups.Sort = "ShapeGroup ASC";
      // 
      // bsoFKSubjectsSubjectParameters
      // 
      this.bsoFKSubjectsSubjectParameters.DataMember = "FK_Subjects_SubjectParameters";
      this.bsoFKSubjectsSubjectParameters.DataSource = this.bsoSubjects;
      // 
      // bsoSubjectParameters
      // 
      this.bsoSubjectParameters.DataMember = "SubjectParameters";
      this.bsoSubjectParameters.DataSource = this.ogamaDataSet;
      this.bsoSubjectParameters.Sort = "SubjectName ASC, Param ASC";
      // 
      // bsoTrialEvents
      // 
      this.bsoTrialEvents.AllowNew = false;
      this.bsoTrialEvents.DataMember = "TrialEvents";
      this.bsoTrialEvents.DataSource = this.ogamaDataSet;
      this.bsoTrialEvents.Sort = "SubjectName ASC, TrialSequence ASC, EventTime ASC";
      // 
      // bsoGazeFixations
      // 
      this.bsoGazeFixations.AllowNew = false;
      this.bsoGazeFixations.DataMember = "GazeFixations";
      this.bsoGazeFixations.DataSource = this.ogamaDataSet;
      this.bsoGazeFixations.Sort = "SubjectName ASC, TrialSequence ASC, CountInTrial ASC";
      // 
      // bsoMouseFixations
      // 
      this.bsoMouseFixations.AllowNew = false;
      this.bsoMouseFixations.DataMember = "MouseFixations";
      this.bsoMouseFixations.DataSource = this.ogamaDataSet;
      this.bsoMouseFixations.Sort = "SubjectName ASC, TrialSequence ASC, CountInTrial ASC";
      // 
      // bsoAOIs
      // 
      this.bsoAOIs.AllowNew = false;
      this.bsoAOIs.DataMember = "AOIs";
      this.bsoAOIs.DataSource = this.ogamaDataSet;
      this.bsoAOIs.Sort = "TrialID ASC, SlideNr ASC, ShapeName ASC";
      // 
      // bsoTrials
      // 
      this.bsoTrials.AllowNew = false;
      this.bsoTrials.DataMember = "Trials";
      this.bsoTrials.DataSource = this.ogamaDataSet;
      this.bsoTrials.Sort = "SubjectName ASC, TrialSequence ASC";
      // 
      // bsoParams
      // 
      this.bsoParams.DataMember = "Params";
      this.bsoParams.DataSource = this.ogamaDataSet;
      this.bsoParams.Sort = "Param ASC";
      // 
      // FormWithInterface
      // 
      this.ClientSize = new System.Drawing.Size(292, 266);
      this.Name = "FormWithInterface";
      this.Load += new System.EventHandler(this.FormWithPicture_Load);
      ((System.ComponentModel.ISupportInitialize)(this.ogamaDataSet)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoSubjects)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoFKSubjectsTrials)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoFKTrialsEvents)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoTrialsAOIs)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoTrialsGazeFixations)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoTrialsMouseFixations)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoShapeGroups)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoFKSubjectsSubjectParameters)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoSubjectParameters)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoTrialEvents)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoGazeFixations)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoMouseFixations)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoAOIs)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoTrials)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsoParams)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    /// <summary>
    /// The <see cref="BindingSource"/> for the subject names.
    /// </summary>
    protected BindingSource bsoSubjects;
    /// <summary>
    /// The <see cref="BindingSource"/> for the subject-trials foreign key.
    /// </summary>
    protected BindingSource bsoFKSubjectsTrials;
    /// <summary>
    /// The dummy <see cref="SQLiteOgamaDataSet"/> that is replaced with the 
    /// dataset defined in each <see cref="Document"/>.
    /// </summary>
    /// <remarks>Needed for Design-Support.</remarks>
    protected SQLiteOgamaDataSet ogamaDataSet;
    /// <summary>
    /// The <see cref="BindingSource"/> for the trials-events foreign key.
    /// </summary>
    protected BindingSource bsoFKTrialsEvents;
    /// <summary>
    /// The <see cref="BindingSource"/> for the trials-aois relation.
    /// </summary>
    protected BindingSource bsoTrialsAOIs;
    /// <summary>
    /// The <see cref="BindingSource"/> for the trials-gaze fixations relation.
    /// </summary>
    protected BindingSource bsoTrialsGazeFixations;
    /// <summary>
    /// The <see cref="BindingSource"/> for the trials-mouse fixations relation.
    /// </summary>
    protected BindingSource bsoTrialsMouseFixations;
    /// <summary>
    /// The <see cref="BindingSource"/> for the shape groups table.
    /// </summary>
    protected BindingSource bsoShapeGroups;
    /// <summary>
    /// The <see cref="BindingSource"/> for the subjects - subject parameters foreign key.
    /// </summary>
    protected BindingSource bsoFKSubjectsSubjectParameters;
    /// <summary>
    /// The <see cref="BindingSource"/> for the subject parameters table.
    /// </summary>
    protected BindingSource bsoSubjectParameters;
    /// <summary>
    /// The <see cref="BindingSource"/> for the TrialEvents table.
    /// </summary>
    protected BindingSource bsoTrialEvents;
    /// <summary>
    /// The <see cref="BindingSource"/> for the Trials table.
    /// </summary>
    protected BindingSource bsoTrials;
    /// <summary>
    /// The <see cref="BindingSource"/> for the GazeFixations table.
    /// </summary>
    protected BindingSource bsoGazeFixations;
    /// <summary>
    /// The <see cref="BindingSource"/> for the MouseFixations table.
    /// </summary>
    protected BindingSource bsoMouseFixations;
    /// <summary>
    /// The <see cref="BindingSource"/> for the AOIs table.
    /// </summary>
    protected BindingSource bsoAOIs;
    /// <summary>
    /// The <see cref="BindingSource"/> for the Params table.
    /// </summary>
    protected BindingSource bsoParams;
  }
}