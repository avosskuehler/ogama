using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OgamaDao.Model.Rta;

namespace Ogama.Modules.Rta
{
    /// <summary>
    /// 
    /// </summary>
    public class RtaExtension4ReplayModule
    {
        private RtaController rtaController = new RtaController();

        private RtaSettings rtaSettings = new RtaSettings();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="btnRTA"></param>
        /// <param name="selectedTrial"></param>
        public void startRtaSession(ToolStripButton btnRTA, string selectedTrial)
        {
            Console.WriteLine("start RTA");
            if (btnRTA.Checked)
            {
                if (rtaController == null)
                {
                    return;
                }

                if (!rtaSettings.isValid())
                {
                    this.configureRtaSettings(selectedTrial);
                    return;
                }

                Boolean setupOk = this.rtaController.setup(rtaSettings);
                if (!setupOk)
                {
                    Ogama.ExceptionHandling.ExceptionMethods.HandleException(new Exception("Could not setup RtaController."));
                }
                rtaController.start();
            }
            else
            {
                rtaController.stop();
            }
        }

        public System.Windows.Forms.MouseEventHandler GetMouseListener()
        {
            return this.rtaController.GetMouseListener();
        }

     
        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="selectedTrial"></param>
        public void configureRtaSettings(string selectedTrial)
        {
            Console.WriteLine("configure RTA");
            RtaSettingsForm rtaSettingsForm = new RtaSettingsForm();
            rtaSettingsForm.Initialize(this.rtaSettings);
            DialogResult result = rtaSettingsForm.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            this.rtaSettings = rtaSettingsForm.getRtaSettings();

          
            if (Document.ActiveDocument == null)
            {
                Ogama.ExceptionHandling.ExceptionMethods.HandleException(new Exception("No active document was found!"));
            }
            
            string filename = getVideoFilenameByDocument(selectedTrial);
            string tempVideoFilename = this.getUniqueFilename(filename + ".tmp.avi");
            string videoFilename = this.getUniqueFilename(filename + ".avi");

            rtaSettings.Filename = videoFilename;
            rtaSettings.TempFilename = tempVideoFilename;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public string getUniqueFilename(string filename)
        {
            int index = 0;
            string test = filename;
            int lastIndexOfDot = filename.LastIndexOf(".");
            string extension = "";
            if (lastIndexOfDot >= 0)
            {
                extension = filename.Substring(lastIndexOfDot);
            }
            
            while (System.IO.File.Exists(test))
            {
                if (extension.Length > 0)
                {
                    
                    test = test.Substring(0, test.Length - (extension.Length));
                    test = test + index + extension;
                }
                else
                {
                    test = test + index;
                }
                index++;
            }

            filename = test;

            return filename;
        }

        private string getVideoFilenameByDocument(string selectedTrial)
        {
            string DocumentFileName = Document.ActiveDocument.ExperimentSettings.DocumentFilename;
            string DocumentPath = Document.ActiveDocument.ExperimentSettings.DocumentPath;
            string VideoFolder = System.IO.Path.Combine(DocumentPath, "RtaVideo");
            if (!System.IO.File.Exists(VideoFolder))
            {
                try
                {
                    System.IO.Directory.CreateDirectory(VideoFolder);
                }
                catch (Exception e)
                {
                    Ogama.ExceptionHandling.ExceptionMethods.ProcessErrorMessage(e.Message);
                }
            }
            string filename = System.IO.Path.Combine(VideoFolder, selectedTrial);
            return filename;
        }

        private LoggerFacade log = new LoggerFacade();
        /// <summary>
        /// 
        /// </summary>
        public void showRtaVideoDialog()
        {
            
            //RtaModule rtaModule = new RtaModule();
            log.info("showRtaVideoDialog()");

        }
    }
}
