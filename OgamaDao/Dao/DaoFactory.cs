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
        private SessionFactoryHolder sessionFactoryHolder;

        //DAOs
        private RtaCategoryDao rtaCategoryDao;
        private RtaEventDao rtaEventDao;


        public void init(string databaseFilename)
        {
            if (sessionFactoryHolder != null)
            {
                return;
            }
            this.databaseFilename = databaseFilename;
            sessionFactoryHolder = new SessionFactoryHolder();
            sessionFactoryHolder.initFileBasedDatabase(this.databaseFilename);
        }



        public RtaCategoryDao GetRtaCategoyDao()
        {
            if (rtaCategoryDao == null)
            {
                rtaCategoryDao = new RtaCategoryDao();
                rtaCategoryDao.SetSessionFactory(sessionFactoryHolder.getHibernateSessionFactory());
            }
            return rtaCategoryDao;
        }

        public RtaEventDao getRtaEventDao()
        {
            if (this.rtaEventDao == null)
            {
                rtaEventDao = new RtaEventDao();
                rtaEventDao.SetSessionFactory(sessionFactoryHolder.getHibernateSessionFactory());
            }
            return rtaEventDao;
        }
    }
}
