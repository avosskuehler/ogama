using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgamaDao.Dao.Rta;

namespace OgamaDao.Dao
{
    public class DaoFactory
    {
        private SessionFactoryHolder sessionFactoryHolder;

        //DAOs
        private RtaCategoryDao rtaCategoryDao;
        private RtaEventDao rtaEventDao;
        private RtaSettingsDao rtaSettingsDao;

        public void init(string databaseFilename)
        {
            if (sessionFactoryHolder != null)
            {
                return;
            }
            sessionFactoryHolder = new SessionFactoryHolder();
            sessionFactoryHolder.initFileBasedDatabase(databaseFilename);
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

        public RtaSettingsDao getRtaSettingsDao()
        {
            if (rtaSettingsDao == null)
            {
                rtaSettingsDao = new RtaSettingsDao();
                rtaSettingsDao.SetSessionFactory(sessionFactoryHolder.getHibernateSessionFactory());
            }
            return rtaSettingsDao;
        }
    }
}
