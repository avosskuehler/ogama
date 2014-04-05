// <copyright file="ImportAOI.cs" company="FU Berlin">
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

namespace Ogama.Modules.ImportExport.AOIData
{
  using System;
  using System.Collections.Generic;
  using System.Data;
  using System.Drawing;
  using System.Globalization;
  using System.Windows.Forms;

  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common.Tools;
  using Ogama.Modules.ImportExport.Common;

  using VectorGraphics.Elements;

  /// <summary>
  /// Class for importing areas of interest through multiple dialogs,
  /// called import assistant.
  /// </summary>
  /// <remarks>All the members are marked as static to make them
  /// available in all the dialogs without the need to ship the
  /// objects. The main entry point is <see cref="ImportAOI.Start()"/>
  /// which starts the import assistant.</remarks>
  public class ImportAOI
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

    /// <summary>
    /// Saves the list to fill with imported <see cref="AOIData"/>.
    /// </summary>
    private static List<AOIData> aoiDataList;

    /// <summary>
    /// Saves the AOI specialized settings used during this import session.
    /// </summary>
    private static AOIDataSettings aoiSettings;

    /// <summary>
    /// Saves the ASCII file import specialized settings
    /// during this import session.
    /// </summary>
    private static ASCIISettings asciiSettings;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes static members of the ImportAOI class.
    /// </summary>
    static ImportAOI()
    {
      aoiDataList = new List<AOIData>();
      aoiSettings = new AOIDataSettings();
      asciiSettings = new ASCIISettings();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the AOI specialized settings used during this import session.
    /// </summary>
    /// <value>A <see cref="AOIDataSettings"/> with the current 
    /// AOI data import settings.</value>
    public static AOIDataSettings AoiSettings
    {
      get { return aoiSettings; }
    }

    /// <summary>
    /// Gets the ASCII file import specialized settings
    /// during this import session.
    /// </summary>
    /// <value>A <see cref="ASCIISettings"/> with the current 
    /// ascii file import settings.</value>
    public static ASCIISettings FileImport
    {
      get { return asciiSettings; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Starts a multiple dialog routine for reading AOIs
    /// from a log file into the programs database.
    /// </summary>
    public static void Start()
    {
      try
      {
        // Show assistant start screen with instructions
        ImportAOIAssistentDialog objfrmImportAssistent = new ImportAOIAssistentDialog();
        if (objfrmImportAssistent.ShowDialog() == DialogResult.OK)
        {
        // Show open file dialog
        OpenFile:
          if (asciiSettings.FileDialog.ShowDialog() == DialogResult.OK)
          {
            // Save import file
            asciiSettings.Filename = asciiSettings.FileDialog.FileName;

            // Show ascii file parse dialog with preview
            ImportParseFileDialog objfrmImportReadFile = new ImportParseFileDialog(ref asciiSettings);
          ReadFile:
            DialogResult resultReadFile = objfrmImportReadFile.ShowDialog();
            if (resultReadFile == DialogResult.OK)
            {
              // Show assign columns dialog
              ImportAOIAssignColumnsDialog objfrmImportAOIAssignColumns
                = new ImportAOIAssignColumnsDialog();

              DialogResult resultAssign = objfrmImportAOIAssignColumns.ShowDialog();
              if (resultAssign == DialogResult.OK)
              {
                // Show import splash window
                asciiSettings.WaitingSplash.RunWorkerAsync();

                // Give some time to show the splash ...
                Application.DoEvents();

                List<string> columnHeaders = new List<string>();

                // Read import log file
                asciiSettings.Rows = FileImport.ParseFile(
                  asciiSettings.Filename,
                  -1, 
                  ref columnHeaders);

                // Do not use the new generated headers, because
                // that is already done in the objfrmImportReadFile dialog
                // and could there been modified by the user.
                // _asciiSettings.ColumnHeaders = columnHeaders;

                // Convert log file to AOI
                ImportAOI.GenerateOgamaAOIDataList();

                // Save the import into ogamas database and the mdf file.
                ImportAOI.SaveImportIntoTablesAndDB();

                // Import has finished.
                asciiSettings.WaitingSplash.CancelAsync();

                // Inform user about success.
                string message = "AOI import data successfully written to database.";
                ExceptionMethods.ProcessMessage("Successfull", message);
              }
              else if (resultAssign == DialogResult.Cancel)
              {
                goto ReadFile;
              }
            }
            else if (resultReadFile == DialogResult.Cancel)
            {
              goto OpenFile;
            }
          }
        }
      }
      catch (Exception ex)
      {
        string message = "Something failed during import." + Environment.NewLine
          + "Please try again with other settings. " + Environment.NewLine +
          "Error: " + ex.Message;
        ExceptionMethods.ProcessErrorMessage(message);

        // CleanUp
        if (asciiSettings.WaitingSplash.IsBusy)
        {
          asciiSettings.WaitingSplash.CancelAsync();
        }
      }
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
    /// This method writes the data that is written in the lists during
    /// import to OGAMAs dataset.
    /// If this could be successfully done the whole new data is
    /// written to the database (.mdf).
    /// </summary>
    private static void SaveImportIntoTablesAndDB()
    {
      try
      {
        // Try to submit the new values to the database
        if (!Queries.WriteAOIDataListToDataSet(aoiDataList, asciiSettings.WaitingSplash))
        {
          throw new DataException("The new areas of interest could not be written into the dataset.");
        }

        // Update aoi table in the mdf database
        int affectedRows = Document.ActiveDocument.DocDataSet.AOIsAdapter.Update(
          Document.ActiveDocument.DocDataSet.AOIs);

        // Submit changes
        Document.ActiveDocument.DocDataSet.AcceptChanges();
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);

        // CleanUp
        Document.ActiveDocument.DocDataSet.RejectChanges();
      }
      finally
      {
        aoiDataList.Clear();
      }
    }

    /// <summary>
    /// This method calls the correct import row
    /// parsing method referring to the current
    /// <see cref="AOIImportModes"/>.
    /// </summary>
    private static void GenerateOgamaAOIDataList()
    {
      switch (aoiSettings.ImportMode)
      {
        case AOIImportModes.UseSimpleRectangles:
          GenerateAOIFromRectangles();
          break;
        case AOIImportModes.UseOgamaColumns:
          GenerateAOIFromOgamaTable();
          break;
      }
    }

    /// <summary>
    /// This method generates <see cref="AOIData"/> from each 
    /// row of the import file, that has to be on OGAMA import format.
    /// </summary>
    private static void GenerateAOIFromOgamaTable()
    {
      // Clear existing values
      aoiDataList.Clear();

      // Use the decimal separator specified.
      NumberFormatInfo nfi = CultureInfo.GetCultureInfo("en-US").NumberFormat;
      if (asciiSettings.DecimalSeparatorCharacter == ',')
      {
        nfi = CultureInfo.GetCultureInfo("de-DE").NumberFormat;
      }

      // Enumerate the columns in the import file and assign their title.
      Dictionary<string, int> columnsImportNum = new Dictionary<string, int>();
      for (int i = 0; i < asciiSettings.ColumnHeaders.Count; i++)
      {
        columnsImportNum.Add(asciiSettings.ColumnHeaders[i], i);
      }

      // Get the assigned titles of the import columns for Ogamas columns
      string importColumnTitleForTrialID = asciiSettings.ColumnAssignments["TrialID"];
      string importColumnTitleForSlideNr = asciiSettings.ColumnAssignments["SlideNr"];
      string importColumnTitleForShapeName = asciiSettings.ColumnAssignments["ShapeName"];
      string importColumnTitleForShapeType = asciiSettings.ColumnAssignments["ShapeType"];
      string importColumnTitleForShapeNumPts = asciiSettings.ColumnAssignments["ShapeNumPts"];
      string importColumnTitleForShapePts = asciiSettings.ColumnAssignments["ShapePts"];
      string importColumnTitleForShapeGroup = asciiSettings.ColumnAssignments["ShapeGroup"];

      // Convert the names into column counters.
      int numTrialIDImportColumn = importColumnTitleForTrialID == string.Empty ? -1 : columnsImportNum[importColumnTitleForTrialID];
      int numSlideNrImportColumn = importColumnTitleForSlideNr == string.Empty ? -1 : columnsImportNum[importColumnTitleForSlideNr];
      int numShapeNameImportColumn = importColumnTitleForShapeName == string.Empty ? -1 : columnsImportNum[importColumnTitleForShapeName];
      int numShapeTypeImportColumn = importColumnTitleForShapeType == string.Empty ? -1 : columnsImportNum[importColumnTitleForShapeType];
      int numShapeNumPtsImportColumn = importColumnTitleForShapeNumPts == string.Empty ? -1 : columnsImportNum[importColumnTitleForShapeNumPts];
      int numShapePtsImportColumn = importColumnTitleForShapePts == string.Empty ? -1 : columnsImportNum[importColumnTitleForShapePts];
      int numShapeGroupImportColumn = importColumnTitleForShapeGroup == string.Empty ? -1 : columnsImportNum[importColumnTitleForShapeGroup];

      foreach (string[] items in asciiSettings.Rows)
      {
        try
        {
          // Create Ogama columns placeholder
          AOIData newAOIData = new AOIData();

          if (numTrialIDImportColumn != -1)
          {
            newAOIData.TrialID = Convert.ToInt32(items[numTrialIDImportColumn]);
          }

          if (numSlideNrImportColumn != -1)
          {
            newAOIData.SlideNr = Convert.ToInt32(items[numSlideNrImportColumn]);
          }

          if (numShapeNameImportColumn != -1)
          {
            newAOIData.ShapeName = items[numShapeNameImportColumn];
          }

          if (numShapeTypeImportColumn != -1)
          {
            newAOIData.ShapeType = (VGShapeType)Enum.Parse(typeof(VGShapeType), items[numShapeTypeImportColumn]);
          }

          if (numShapeNumPtsImportColumn != -1)
          {
            newAOIData.ShapeNumPts = Convert.ToInt32(items[numShapeNumPtsImportColumn]);
          }

          if (numShapePtsImportColumn != -1)
          {
            newAOIData.ShapePts = items[numShapePtsImportColumn];
          }

          if (numShapeGroupImportColumn != -1)
          {
            newAOIData.Group = items[numShapeGroupImportColumn];
          }

          if (newAOIData.Group == null || newAOIData.Group == string.Empty)
          {
            newAOIData.Group = " ";
          }

          aoiDataList.Add(newAOIData);
        }
        catch (Exception ex)
        {
          ExceptionMethods.HandleException(ex);
        }
      }
    }

    /// <summary>
    /// This method generates <see cref="AOIData"/> from each 
    /// row of the import file, which has coordinates of simple rectangles in it.
    /// </summary>
    private static void GenerateAOIFromRectangles()
    {
      // Clear existing values
      aoiDataList.Clear();

      // Use the decimal separator specified.
      NumberFormatInfo nfi = CultureInfo.GetCultureInfo("en-US").NumberFormat;
      if (asciiSettings.DecimalSeparatorCharacter == ',')
      {
        nfi = CultureInfo.GetCultureInfo("de-DE").NumberFormat;
      }

      // Enumerate the columns in the import file and assign their title.
      Dictionary<string, int> columnsImportNum = new Dictionary<string, int>();
      for (int i = 0; i < asciiSettings.ColumnHeaders.Count; i++)
      {
        columnsImportNum.Add(asciiSettings.ColumnHeaders[i], i);
      }

      // Get the assigned titles of the import columns for Ogamas columns
      string importColumnTitleForTrialID = asciiSettings.ColumnAssignments["TrialID"];
      string importColumnTitleForSlideNr = asciiSettings.ColumnAssignments["SlideNr"];
      string importColumnTitleForShapeName = asciiSettings.ColumnAssignments["NameOfShape"];
      string importColumnTitleForTopLeftCornerX = asciiSettings.ColumnAssignments["Left top corner X"];
      string importColumnTitleForTopLeftCornerY = asciiSettings.ColumnAssignments["Left top corner Y"];
      string importColumnTitleForBottomRightCornerX = asciiSettings.ColumnAssignments["Right bottom corner X"];
      string importColumnTitleForBottomRightCornerY = asciiSettings.ColumnAssignments["Right bottom corner Y"];
      string importColumnTitleForShapeGroup = asciiSettings.ColumnAssignments["ShapeGroup"];

      // Convert the names into column counters.
      int numTrialIDImportColumn = importColumnTitleForTrialID == string.Empty ? -1 : columnsImportNum[importColumnTitleForTrialID];
      int numSlideNrImportColumn = importColumnTitleForSlideNr == string.Empty ? -1 : columnsImportNum[importColumnTitleForSlideNr];
      int numShapeNameImportColumn = importColumnTitleForShapeName == string.Empty ? -1 : columnsImportNum[importColumnTitleForShapeName];
      int numTopLeftCornerXImportColumn = importColumnTitleForTopLeftCornerX == string.Empty ? -1 : columnsImportNum[importColumnTitleForTopLeftCornerX];
      int numTopLeftCornerYImportColumn = importColumnTitleForTopLeftCornerY == string.Empty ? -1 : columnsImportNum[importColumnTitleForTopLeftCornerY];
      int numBottomRightCornerXImportColumn = importColumnTitleForBottomRightCornerX == string.Empty ? -1 : columnsImportNum[importColumnTitleForBottomRightCornerX];
      int numBottomRightCornerYImportColumn = importColumnTitleForBottomRightCornerY == string.Empty ? -1 : columnsImportNum[importColumnTitleForBottomRightCornerY];
      int numShapeGroupImportColumn = importColumnTitleForShapeGroup == string.Empty ? -1 : columnsImportNum[importColumnTitleForShapeGroup];

      foreach (string[] items in asciiSettings.Rows)
      {
        try
        {
          RectangleF boundingRect = new RectangleF();

          // Calc bounding Rect from X1,X2,Y1,Y2 as given
          string x1 = "0";
          string y1 = "0";
          string x2 = "0";
          string y2 = "0";

          // Create Ogama columns placeholder
          AOIData newAOIData = new AOIData();

          if (numTrialIDImportColumn != -1)
          {
            newAOIData.TrialID = Convert.ToInt32(items[numTrialIDImportColumn]);
          }

          if (numSlideNrImportColumn != -1)
          {
            newAOIData.SlideNr = Convert.ToInt32(items[numSlideNrImportColumn]);
          }

          if (numShapeNameImportColumn != -1)
          {
            newAOIData.ShapeName = items[numShapeNameImportColumn];
          }

          newAOIData.ShapeType = VGShapeType.Rectangle;
          newAOIData.ShapeNumPts = 4;

          if (numTopLeftCornerXImportColumn != -1)
          {
            x1 = items[numTopLeftCornerXImportColumn];
          }

          if (numTopLeftCornerYImportColumn != -1)
          {
            y1 = items[numTopLeftCornerYImportColumn];
          }

          if (numBottomRightCornerXImportColumn != -1)
          {
            x2 = items[numBottomRightCornerXImportColumn];
          }

          if (numBottomRightCornerYImportColumn != -1)
          {
            y2 = items[numBottomRightCornerYImportColumn];
          }

          boundingRect.X = Convert.ToSingle(x1);
          boundingRect.Y = Convert.ToSingle(y1);
          boundingRect.Width = Convert.ToSingle(x2) - boundingRect.X;
          boundingRect.Height = Convert.ToSingle(y2) - boundingRect.Y;

          string pts = "P1:(" + string.Format("{0:F1}", boundingRect.Left) + ";" +
                        string.Format("{0:F1}", boundingRect.Top) + ") P2:(" +
                        string.Format("{0:F1}", boundingRect.Right) + ";" +
                        string.Format("{0:F1}", boundingRect.Top) + ") P3:(" +
                        string.Format("{0:F1}", boundingRect.Right) + ";" +
                        string.Format("{0:F1}", boundingRect.Bottom) + ") P4:(" +
                        string.Format("{0:F1}", boundingRect.Left) + ";" +
                        string.Format("{0:F1}", boundingRect.Bottom) + ")";
          newAOIData.ShapePts = pts;

          if (numShapeGroupImportColumn != -1)
          {
            newAOIData.Group = items[numShapeGroupImportColumn];
          }

          if (newAOIData.Group == null || newAOIData.Group == string.Empty)
          {
            newAOIData.Group = " ";
          }

          // Add new Element to AOITable
          aoiDataList.Add(newAOIData);
        }
        catch (Exception ex)
        {
          ExceptionMethods.HandleException(ex);
        }
      }
    }
    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
