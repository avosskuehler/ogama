// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NameShapeDlg.cs" company="Freie Universität Berlin">
//   OGAMA - open gaze and mouse analyzer 
//   Copyright (C) 2015 Dr. Adrian Voßkühler  
//   Licensed under GPL V3
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>
// <summary>
//   A pop up <see cref="Form" />. Asks for the name of a newly defined area of interest shape.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Ogama.Modules.AOI.Dialogs
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Windows.Forms;

  using Ogama.DataSet;
  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common.Tools;

  /// <summary>
  ///   A pop up <see cref="Form" />. Asks for the name of a newly defined area of interest shape.
  /// </summary>
  public partial class NameShapeDlg : Form
  {
    #region Fields

    /// <summary>
    /// The trial id.
    /// </summary>
    private readonly int trialID;

    #endregion

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the NameShapeDlg class.
    /// </summary>
    /// <param name="trialID">
    /// The trial ID of the shape
    /// </param>
    public NameShapeDlg(int trialID)
    {
      this.InitializeComponent();
      this.trialID = trialID;
    }

    #endregion

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region Public Properties

    /// <summary>
    ///   Gets or sets the shape name written in textbox field.
    /// </summary>
    /// <value>A <see cref="string" /> with the new shape name.</value>
    public string ShapeName
    {
      get
      {
        return this.txbShapeName.Text;
      }

      set
      {
        this.txbShapeName.Text = value;
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Handles the Click event of the btnOK control.
    ///   Checks for correct shape name.
    /// </summary>
    /// <param name="sender">
    /// The source of the event.
    /// </param>
    /// <param name="e">
    /// The <see cref="System.EventArgs"/> instance containing the event data.
    /// </param>
    private void BtnOkClick(object sender, EventArgs e)
    {
      this.ShapeName = this.ShapeName.Trim();

      var sb = new StringBuilder();
      bool changed = false;
      for (int i = 0; i < this.ShapeName.Length; i++)
      {
        if (char.IsLetterOrDigit(this.ShapeName[i]))
        {
          sb.Append(this.ShapeName[i]);
        }
        else
        {
          changed = true;
        }
      }

      this.ShapeName = sb.ToString();

      if (changed)
      {
        string message = "Please note: Non letter and non digit characters are removed from the shape name."
                         + Environment.NewLine + "Other characters are not allowed for the database entry.";

        ExceptionMethods.ProcessMessage("Shape name modified", message);

        return;
      }

      if (this.ShapeName == string.Empty)
      {
        InformationDialog.Show(
          "Empty shape name",
          "Please enter at least one character for the shape name",
          false,
          MessageBoxIcon.Information);

        return;
      }

      if (IOHelpers.IsNumeric(this.ShapeName[0]))
      {
        InformationDialog.Show(
          "Invalid shape name",
          "Please do not use a number for the first character of the shape name.",
          false,
          MessageBoxIcon.Information);
        return;
      }

      var aois = Document.ActiveDocument.DocDataSet.AOIs.Where(o => o.ShapeName == this.ShapeName && o.TrialID == this.trialID);

      // OgamaDataSet.AOIsDataTable aoisTable = Document.ActiveDocument.DocDataSet.AOIsAdapter.GetDataByTrialIDAndShapeName(this.trialID, this.ShapeName);
      if (aois.Any())
      {
        string message = "The area of interest: " + Environment.NewLine + this.ShapeName + " already exists."
                         + Environment.NewLine + "Please select another name.";
        ExceptionMethods.ProcessMessage("Shape exists", message);
        return;
      }

      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    #endregion
  }
}