// <copyright file="ASCIISettings.cs" company="FU Berlin">
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

namespace Ogama.Modules.ImportExport.Common
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.IO;
  using System.Windows.Forms;
  using System.Xml.Serialization;

  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common.Tools;
  using Ogama.Modules.Common.Types;

  /// <summary>
  /// This class encapsulates fields and methods need for parsing
  /// ASCII files. It is used in each of the other import classes.
  /// </summary>
  [Serializable]
  public class ASCIISettings
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
    /// An <see cref="OpenFileDialog"/> for ASCII files.
    /// </summary>
    private OpenFileDialog fileDialog;

    /// <summary>
    /// An <see cref="BackgroundWorker"/> to show a splash screen
    /// during import.
    /// </summary>
    private BackgroundWorker bgwLoad;

    /// <summary>
    /// File name string of import file.
    /// </summary>
    private string filename;

    /// <summary>
    /// Saves the imported files rows.
    /// </summary>
    private List<string[]> rows;

    /// <summary>
    /// Saves the import files column headers.
    /// </summary>
    private List<string> columnHeaders;

    /// <summary>
    /// Dictionary that saves the selected column assignments
    /// of the import file and the programs database
    /// </summary>
    private XMLSerializableDictionary<string, string> columnAssignments;

    /// <summary>
    /// Flag. True, if import should ignore lines,
    /// with less columns, than first data row.
    /// e.g. #MSG lines in iVievX format.
    /// </summary>
    private bool ignoreSmallLines;

    /// <summary>
    /// Flag. True, if import should ignore line,
    /// with 2nd consecutive timestamp.    
    /// </summary>
    private bool ignoreDoubles;

    /// <summary>
    /// Flag. True, if import should ignore lines,
    /// that don´t start with a numeral
    /// </summary>
    private bool ignoreNotNumeralLines;

    /// <summary>
    /// Import column separator character.
    /// </summary>
    private char columnSeparatorCharacter;

    /// <summary>
    /// Import decimal separator character.
    /// </summary>
    private char decimalSeparatorCharacter;

    /// <summary>
    /// Flag. Ignore lines commented with character <see cref="IgnoreQuotationString"/>
    /// </summary>
    private bool ignoreQuotes;

    /// <summary>
    /// Quotion character for ignored lines
    /// </summary>
    private string ignoreQuotationString;

    /// <summary>
    /// Flag. Use only lines commented with string <see cref="UseQuotationString"/>
    /// </summary>
    private bool useQuotes;

    /// <summary>
    /// Quotion character for used lines
    /// </summary>
    private string useQuotationString;

    /// <summary>
    /// Flag. Ignore lines that contain <see cref="IgnoreTriggerString"/>.
    /// </summary>
    private bool ignoreTriggerStringLines;

    /// <summary>
    /// Trigger string.
    /// </summary>
    private string ignoreTriggerString;

    /// <summary>
    /// Flag. True, if column titles are in the first data row.
    /// </summary>
    private bool columnTitlesAtFirstRow;



    /// <summary>
    /// Saves the time value of the very first raw data value.
    /// </summary>
    private long startTime;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the ASCIISettings class.
    /// </summary>
    public ASCIISettings()
    {
      // Init OpenFileDialog
      this.fileDialog = new System.Windows.Forms.OpenFileDialog();
      this.fileDialog.DefaultExt = "txt";
      this.fileDialog.FileName = "*.txt;*.asc;*.csv";
      this.fileDialog.FilterIndex = 1;
      this.fileDialog.Filter = "Known text files|*.txt;*.asc;*.csv|txt files (*.txt)|*.txt|ASCII files (*.asc)|*.asc|Comma separated-values (*.csv)|*.csv|All files (*.*)|*.*";
      this.fileDialog.Title = "Please select the raw data ASCII file with the gaze and/or mouse samples.";

      // Init background worker splash
      this.bgwLoad = new System.ComponentModel.BackgroundWorker();
      this.bgwLoad.WorkerSupportsCancellation = true;
      this.bgwLoad.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwLoad_DoWork);

      // Init fields.
      this.rows = new List<string[]>();
      this.columnHeaders = new List<string>();
      this.filename = string.Empty;
      this.columnSeparatorCharacter = '\t';
      this.decimalSeparatorCharacter = '.';
      this.ignoreQuotes = true;
      this.ignoreSmallLines = true;
      this.ignoreNotNumeralLines = true;
      this.ignoreQuotationString = "#";
      this.ignoreTriggerStringLines = true;
      this.ignoreTriggerString = "Keyboard";
      this.ignoreDoubles = false;
      this.columnTitlesAtFirstRow = true;
      this.columnTitlesAtPreviousRow = false;
      this.columnAssignments = new XMLSerializableDictionary<string, string>();
      this.useQuotes = false;
      this.useQuotationString = "EFIX";      
      this.startTime = 0;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets an <see cref="OpenFileDialog"/> for ASCII files.
    /// </summary>
    /// <value>A <see cref="OpenFileDialog"/> for opening ASCII files.</value>
    [XmlIgnore]
    public OpenFileDialog FileDialog
    {
      get { return this.fileDialog; }
    }

    /// <summary>
    /// Gets a <see cref="BackgroundWorker"/> to show a splash screen
    /// during import.
    /// </summary>
    /// <value>A <see cref="BackgroundWorker"/> for showing a splash screen.</value>
    [XmlIgnore]
    public BackgroundWorker WaitingSplash
    {
      get { return this.bgwLoad; }
    }

    /// <summary>
    /// Gets or sets the list to fill with the file rows.
    /// </summary>
    /// <value>A list of string arrays with the readed rows, separated in columns.</value>
    [XmlIgnore]
    public List<string[]> Rows
    {
      get { return this.rows; }
      set { this.rows = value; }
    }

    /// <summary>
    /// Gets or sets the list to fill with the files column headers.
    /// </summary>
    /// <value>A <see cref="List{String}"/>with the column headers of the file.</value>
    public List<string> ColumnHeaders
    {
      get { return this.columnHeaders; }
      set { this.columnHeaders = value; }
    }

    /// <summary>
    /// Gets or sets the file name string of the file to import.
    /// </summary>
    /// <value>A <see cref="string"/> with the import files name.</value>
    public string Filename
    {
      get { return this.filename; }
      set { this.filename = value; }
    }

    /// <summary>
    /// Gets or sets the <see cref="XMLSerializableDictionary{String,String}"/>
    /// that saves the selected column assignments
    /// of the import file and the programs database
    /// </summary>
    /// <value>A <see cref="XMLSerializableDictionary{String,String}"/>
    /// with the assignment of the ogama and file columns.</value>
    public XMLSerializableDictionary<string, string> ColumnAssignments
    {
      get { return this.columnAssignments; }
      set { this.columnAssignments = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the import should ignore lines,
    /// with less columns, than first data row.
    /// e.g. #MSG lines in iVievX format.
    /// </summary>
    /// <value>A <see cref="Boolean"/> that is <strong>true</strong>,
    /// if import should ignore lines, with less columns, than in first data row,
    /// otherwise <strong>false</strong>.</value>
    public bool IgnoreSmallLines
    {
      get { return this.ignoreSmallLines; }
      set { this.ignoreSmallLines = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether 
    /// import should ignore lines,
    /// that don´t start with a numeral
    /// </summary>
    /// <value>A <see cref="Boolean"/> that is <strong>true</strong>,
    /// if import should ignore lines, hat don´t start with a numeral,
    /// otherwise <strong>false</strong>.</value>
    public bool IgnoreNotNumeralLines
    {
      get { return this.ignoreNotNumeralLines; }
      set { this.ignoreNotNumeralLines = value; }
    }

    /// <summary>
    /// Gets or sets the import column separator character.
    /// </summary>
    /// <value>A <see cref="char"/> with the column separator.</value>
    public char ColumnSeparatorCharacter
    {
      get { return this.columnSeparatorCharacter; }
      set { this.columnSeparatorCharacter = value; }
    }

    /// <summary>
    /// Gets or sets the import decimal separator character.
    /// </summary>
    /// <value>A <see cref="char"/> with the decimal separator.</value>
    public char DecimalSeparatorCharacter
    {
      get { return this.decimalSeparatorCharacter; }
      set { this.decimalSeparatorCharacter = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether 
    /// lines commented with character <see cref="IgnoreQuotationString"/> should be ignored.
    /// </summary>
    /// <value>A <see cref="Boolean"/> that is <strong>true</strong>,
    /// if import should ignore lines, commented with character <see cref="IgnoreQuotationString"/>,
    /// otherwise <strong>false</strong>.</value>
    public bool IgnoreQuotes
    {
      get { return this.ignoreQuotes; }
      set { this.ignoreQuotes = value; }
    }

    /// <summary>
    /// Gets or sets the quotion character.
    /// </summary>
    /// <value>A <see cref="string"/> with the quotation string.</value>
    public string IgnoreQuotationString
    {
      get { return this.ignoreQuotationString; }
      set { this.ignoreQuotationString = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether 
    /// only lines commented with string <see cref="UseQuotationString"/>
    /// should be used during import.
    /// </summary>
    /// <value>A <see cref="Boolean"/> that is <strong>true</strong>,
    /// if import should only use lines, commented with character <see cref="UseQuotationString"/>,
    /// otherwise <strong>false</strong>.</value>
    public bool UseQuotes
    {
      get { return this.useQuotes; }
      set { this.useQuotes = value; }
    }

    /// <summary>
    /// Gets or sets the quotion character for used lines.
    /// </summary>
    /// <value>A <see cref="string"/> with the quotation string.</value>
    public string UseQuotationString
    {
      get { return this.useQuotationString; }
      set { this.useQuotationString = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether 
    /// lines that contain <see cref="IgnoreTriggerString"/>
    /// should be ignored.
    /// </summary>
    /// <value>A <see cref="Boolean"/> that is <strong>true</strong>,
    /// if import should ignore lines containing <see cref="IgnoreTriggerString"/>,
    /// otherwise <strong>false</strong>.</value>
    public bool IgnoreTriggerStringLines
    {
      get { return this.ignoreTriggerStringLines; }
      set { this.ignoreTriggerStringLines = value; }
    }

    /// <summary>
    /// Gets or sets the trigger string for ignored lines.
    /// </summary>
    /// <value>A <see cref="string"/> with the trigger string,
    /// that indicates lines that should be ignored during import.</value>
    public string IgnoreTriggerString
    {
      get { return this.ignoreTriggerString; }
      set { this.ignoreTriggerString = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether 
    /// column titles are in the first data row that is read.
    /// </summary>
    /// <value>A <see cref="Boolean"/> that is <strong>true</strong>,
    /// if column titles are in the first data row,
    /// otherwise <strong>false</strong>.</value>
    public bool ColumnTitlesAtFirstRow
    {
      get { return this.columnTitlesAtFirstRow; }
      set { this.columnTitlesAtFirstRow = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether 
    /// column titles are in the first data row that is read.
    /// </summary>
    /// <value>A <see cref="Boolean"/> that is <strong>true</strong>,
    /// if column titles are above the first data row,
    /// otherwise <strong>false</strong>.</value>
    public bool ColumnTitlesAtPreviousRow 
    {
      get { return this.columnTitlesAtPreviousRow; }
      set { this.columnTitlesAtPreviousRow = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether 
    /// double timestamps should be used during import.
    /// </summary>
    /// <value>A <see cref="Boolean"/> that is <strong>true</strong>,
    /// if import should only use lines, with a double timestamp
    /// otherwise <strong>false</strong>.</value>
    public bool IgnoreDoubles
    {
        get { return this.ignoreDoubles; }
        set { this.ignoreDoubles = value; }
    }

   

    /// <summary>
    /// Gets or sets the time value of the very first raw data value.
    /// </summary>
    /// <value>An <see cref="Int64"/> with the starting time of the experiment.</value>
    public long StartTime
    {
      get { return this.startTime; }
      set { this.startTime = value; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// This method parses the given file under the conditions specified
    /// in this <see cref="ASCIISettings"/> and returns the values as a collection
    /// of <see cref="string"/> arrays.
    /// </summary>
    /// <param name="importFile">The file to parse.</param>
    /// <param name="numberOfImportLines">An <see cref="int"/>
    /// with the max number of lines to import.
    /// Set it to -1 to use all lines.</param>
    /// <param name="columnHeaders">ref. A list of <see cref="string"/>s 
    /// that represent the import file column headers.</param>
    /// <returns>A list of <see cref="string"/> arrays with the separated items of
    /// the file, that are read using the current <see cref="ASCIISettings"/></returns>
    /// <exception cref="System.IO.FileNotFoundException">Thrown, when
    /// the file to import does not exist.</exception>
    public List<string[]> ParseFile(
      string importFile, 
      int numberOfImportLines,
      ref List<string> columnHeaders)
    {
      // Check import file.
      if (!File.Exists(importFile))
      {
        throw new FileNotFoundException("The import file could not be found");
      }

      // Create return list.
      List<string[]> fileRows = new List<string[]>();

      // Check out parameter.
      if (columnHeaders == null)
      {
        columnHeaders = new List<string>();
      }
      else
      {
        columnHeaders.Clear();
      }

      string line = string.Empty;
      int counter = 0;
      int columncount = 0;

      // Begin reading File
      try
      {
        using (StreamReader importReader = new StreamReader(importFile))
        {
            processContent(numberOfImportLines, columnHeaders, fileRows, ref counter, ref columncount, importReader);
        }
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }

      return fileRows;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="numberOfImportLines"></param>
    /// <param name="columnHeaders"></param>
    /// <param name="fileRows"></param>
    /// <param name="counter"></param>
    /// <param name="columncount"></param>
    /// <param name="importReader"></param>
    public void processContent(int numberOfImportLines, 
        List<string> columnHeaders, 
        List<string[]> fileRows, 
        ref int counter, 
        ref int columncount, 
        StreamReader importReader)
    {
        string line = String.Empty;
        string lastLine = String.Empty;
        // Read ImportFile
        while ((line = importReader.ReadLine()) != null)
        {
            // ignore empty lines
            if (line.Trim() == string.Empty)
            {
                lastLine = line;
                continue;
            }

            // Ignore Quotes if applicable
            if (this.IgnoreQuotes &&
              line.Trim().Substring(0, this.IgnoreQuotationString.Length) ==
              this.IgnoreQuotationString)
            {
                lastLine = line; 
                continue;
            }

            // Ignore lines that do not have the "use only" quotation
            // string
            if (this.UseQuotes &&
              !line.Contains(this.UseQuotationString))
            {
                lastLine = line; 
                continue;
            }

            // ignore lines with ignore trigger
            if (this.IgnoreTriggerStringLines &&
              line.Contains(this.IgnoreTriggerString))
            {
                lastLine = line; 
                continue;
            }

            // Split Tab separated line items
            string[] items = line.Split(this.ColumnSeparatorCharacter);
           

            // Use only numeric starting lines if applicable
            if (this.IgnoreNotNumeralLines && !IOHelpers.IsNumeric(line[0]))
            {
                lastLine = line; 
                continue;
            }

            // Skip small lines if applicable
            if (this.IgnoreSmallLines && columncount != items.Length)
            {
                continue;
            }

            if (counter == 0)
            {
                columncount = items.Length;

                // Fill column header list
                for (int i = 0; i < items.Length; i++)
                {
                    string headerText = this.ColumnTitlesAtFirstRow ? items[i].Replace(' ', '-') : "Column" + i.ToString();
                    columnHeaders.Add(headerText);

                }

              handleColumnTitlesAtPreviousRow(columnHeaders, lastLine);
              
            }

            // Skip small lines if applicable
            if (this.IgnoreSmallLines && columncount != items.Length)
            {
                lastLine = line;
                continue;
            }

            // Skip first line if filled with column titles
            if (this.ColumnTitlesAtFirstRow && counter == 0)
            {
                counter++;
                continue;
            }

            // Add row to import list
            fileRows.Add(items);

            // Increase counter
            counter++;

            // Cancel import, if only a part for preview should be imported.
            if (counter > numberOfImportLines && numberOfImportLines >= 0)
            {
                break;
            }

            lastLine = line;

          }//end while
    }

    protected void handleColumnTitlesAtPreviousRow(List<string> columnHeaders, string lastLine)
    {
        if (this.ColumnTitlesAtPreviousRow)
        {
            string[] lastItems = lastLine.Split(this.ColumnSeparatorCharacter);
            if (lastItems.Length > 0)
            {
                columnHeaders.Clear();
                for (int i = 0; i < lastItems.Length; i++)
                {
                    string headerText = lastItems[i];
                    headerText = headerText.Replace(' ', '-');
                    columnHeaders.Add(headerText);
                }
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

    /// <summary>
    /// The <see cref="BackgroundWorker.DoWork"/> event handler of the
    /// <see cref="BackgroundWorker"/> <see cref="bgwLoad"/>.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="DoWorkEventArgs"/> with the event data.</param>
    private void bgwLoad_DoWork(object sender, DoWorkEventArgs e)
    {
      // Get the BackgroundWorker that raised this event.
      BackgroundWorker worker = sender as BackgroundWorker;

      ImportDataSplash newSplash = new ImportDataSplash();
      newSplash.Worker = worker;
      newSplash.ShowDialog();
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
    /// Saves the current import setting to a OGAMA import settings file.
    /// Extension ".ois"
    /// </summary>
    /// <param name="filePath">A <see cref="string"/> with the path to the 
    /// OGAMA target import settings xml file.</param>
    /// <returns><strong>True</strong> if successful, 
    /// otherwise <strong>false</strong>.</returns>
    private bool Serialize(string filePath)
    {
      try
      {
        using (TextWriter writer = new StreamWriter(filePath))
        {
          XmlSerializer serializer = new XmlSerializer(typeof(ASCIISettings));
          serializer.Serialize(writer, this);
        }
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);

        return false;
      }

      return true;
    }

    /// <summary>
    /// Reads an OGAMA import settings file.
    /// </summary>
    /// <param name="filePath">A <see cref="string"/> with the path to the 
    /// OGAMA import settings xml file.</param>
    /// <returns><strong>True</strong> if successful, 
    /// otherwise <strong>null</strong>.</returns>
    private ASCIISettings Deserialize(string filePath)
    {
      try
      {
        ASCIISettings settings = new ASCIISettings();

        using (FileStream fs = new FileStream(filePath, FileMode.Open))
        {
          // Create an instance of the XmlSerializer class;
          // specify the type of object to be deserialized 
          XmlSerializer serializer = new XmlSerializer(typeof(ASCIISettings));

          //////* If the XML Document has been altered with unknown 
          //////nodes or attributes, handle them with the 
          //////UnknownNode and UnknownAttribute events.*/
          ////serializer.UnknownNode += new XmlNodeEventHandler(serializer_UnknownNode);
          ////serializer.UnknownAttribute += new XmlAttributeEventHandler(serializer_UnknownAttribute);

          /* Use the Deserialize method to restore the object's state with
          data from the XML Document. */
          settings = (ASCIISettings)serializer.Deserialize(fs);
        }

        return settings;
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }

      return null;
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER




    public bool columnTitlesAtPreviousRow { get; set; }
  }
}