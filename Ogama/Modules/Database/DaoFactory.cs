using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgamaDao.Dao.Rta;

namespace Ogama.Modules.Database
{
    public class DaoFactory
    {
        private RtaCategoryDao rtaCategoryDao;

        public RtaCategoryDao getRtaCategoryDao()
        {
            if (rtaCategoryDao == null)
            {
                rtaCategoryDao = new RtaCategoryDao();
                rtaCategoryDao.initFileBasedDatabase(getDatabaseFilename());
            }
            return rtaCategoryDao;
        }

        protected string getDatabaseFilename()
        {
            string documentFilepath = Document.ActiveDocument.ExperimentSettings.DatabasePath;
            string dbFilename = System.IO.Path.Combine(documentFilepath, Document.ActiveDocument.ExperimentSettings.Name + ".db3");
            return dbFilename;
        }
    }
}
