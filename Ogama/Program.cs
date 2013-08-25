// <copyright file="Program.cs" company="FU Berlin">
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

namespace Ogama
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Windows.Forms;

  using Ogama.ExceptionHandling;
  using Ogama.MainWindow;
  using Ogama.MainWindow.Dialogs;
  using Ogama.Modules.ImportExport.AOIData;
    using OgamaDao.Model.Rta;
    using Ogama.Modules.Rta;

  /// <summary>
  /// Main Program class with entry point for application.
  /// </summary>
  public static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// Shows a splash welcome screen and loads
    /// the main MDI frame by running <see cref="MainWindow"/>.
    /// </summary>
    /// <param name="args">A <see cref="string"/> array with the command line
    /// arguments for OGAMA, e.g. file to load.</param>
    [STAThread]
    public static void Main(string[] args)
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      runOgama(args);

      //runRtaDemo();

       //testVideoFilter();
       //runFormDemo();

    }

    private static void runRtaDemo()
    {

        setTestDocument();
        RtaSettings rtaSettings = getRtaSettings();

        RtaModule rtaModule = new RtaModule(rtaSettings);
        Ogama.Modules.Rta.RtaReplay.FormRtaView form = rtaModule.GetFormToRun();

        Application.Run(form);

    }

    private static void setTestDocument()
    {
        Document testDoc = new Document();
        testDoc.ExperimentSettings = new Ogama.Properties.ExperimentSettings();
        testDoc.ExperimentSettings.DocumentPath = "c:/temp";
        testDoc.ExperimentSettings.Name = "rtaDemo2";
        Document.ActiveDocument = testDoc;
    }

    private static RtaSettings getRtaSettings()
    {
        RtaSettings rtaSettings = new RtaSettings();
        string movieFilename = "c:/temp/demo2.mp4";
        rtaSettings.Filename = movieFilename;
        rtaSettings.ID = new Guid("fe6bdeb7-fea8-4ae5-a2bb-f2ed28210e68");
        OgamaDao.Dao.DaoFactory df = Ogama.Modules.Database.DaoFactoryWrapper.GetDaoFactory();
        IList<RtaSettings> rtaSettingsList = df.getRtaSettingsDao().find(rtaSettings);
        RtaSettings[] array = new RtaSettings[rtaSettingsList.Count];
        rtaSettingsList.CopyTo(array, 0);
        
        if (array.Length == 0)
        {
            df.getRtaSettingsDao().save(rtaSettings); 
        }
        else
        {
            rtaSettings = array[0];
        }
        return rtaSettings;
    }

    private static void testVideoFilter()
    {
        List<string> list = new Ogama.Modules.Rta.RtaController().getAvailbleVideoFilterNames();
        for (int i = 0; i < list.Count; i++)
        {
            Console.WriteLine("filterName:" + list[i]);
        }
    }

    private static void runFormDemo()
     {
         Ogama.Modules.ImportExport.Common.ASCIISettings settings = 
             new Ogama.Modules.ImportExport.Common.ASCIISettings();
         //Application.Run(new Ogama.Modules.ImportExport.Common.ImportParseFileDialog(settings));

         Application.Run(new ImportAOIAssignColumnsDialog());
     }

   

   

    private static void runOgama(string[] args)
    {
        // Support for command line arguments.
        string fileName = string.Empty;
        if (args.Length == 1)
        {
            if (File.Exists(args[0]))
            {
                fileName = args[0];
            }
        }

        try
        {
            // Show splash screen with copyright
            InitialSplash objfrmSplash = new InitialSplash();
            objfrmSplash.ShowDialog();

            // Start Application
            Application.Run(new Ogama.MainWindow.MainForm(fileName));
        }
        catch (Exception ex)
        {
            ExceptionMethods.ProcessUnhandledException(ex);
        }
    }
  }
}