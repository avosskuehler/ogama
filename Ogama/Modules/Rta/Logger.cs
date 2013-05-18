using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace Ogama.Modules.Rta
{
    public class LoggerFacade
    {
        Logger logger = LogManager.GetLogger("LoggerFacade");

        public void info(string s)
        {
            //string path = Ogama.Document.ActiveDocument.ExperimentSettings.DocumentPath;
            logger.Info(s);
            
        }
    }
}
