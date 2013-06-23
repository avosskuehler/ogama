using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ogama.Modules.Database
{
    /// <summary>
    /// 
    /// </summary>
    public class DaoFactoryWrapper
    {
        private static OgamaDao.Dao.DaoFactory DaoFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <returns>a singleton instance of the DaoFacory</returns>
        public static OgamaDao.Dao.DaoFactory GetDaoFactory()
        {
            if (DaoFactory == null)
            {
                Document Document = Document.ActiveDocument;
                DaoFactory = new OgamaDao.Dao.DaoFactory();
                string path = Document.ExperimentSettings.DatabasePath;
                String experimentName = Document.ExperimentSettings.Name;
                string databaseFile = System.IO.Path.Combine(path,experimentName);
                DaoFactory.init(databaseFile);
            }
            return DaoFactory;
        }
    }
}
