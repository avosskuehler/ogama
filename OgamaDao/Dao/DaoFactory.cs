using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgamaDao.Dao.Rta;

namespace OgamaDao.Dao
{
    public class DaoFactory
    {
        private string databaseFilename;
        private RtaCategoryDao rtaCategoryDao;

        public void init(string databaseFilename)
        {
            this.databaseFilename = databaseFilename;
        }



        public RtaCategoryDao GetRtaCategoyDao()
        {
            if (rtaCategoryDao == null)
            {
                rtaCategoryDao = new RtaCategoryDao();
                rtaCategoryDao.initFileBasedDatabase(this.databaseFilename);
            }
            return rtaCategoryDao;
        }
    }
}
