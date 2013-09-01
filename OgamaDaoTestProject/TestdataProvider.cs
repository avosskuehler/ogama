using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgamaDao.Dao;
using OgamaDao.Model.Rta;

namespace OgamaDaoTestProject
{
    public class TestdataProvider
    {
        public static string getTestDatabaseFilename()
        {
            return "c:/temp/ogamaDaoTestDb.sqlite3";
        }

        public RtaSettings createTestModel(DaoFactory daoFactory)
        {
            RtaSettings rtaSettings = new RtaSettings();
            RtaCategory rtaCategory = new RtaCategory();
            RtaEvent rtaEvent = new RtaEvent();

            daoFactory.getRtaSettingsDao().save(rtaSettings);

            rtaCategory.fkRtaSettings = rtaSettings;
            daoFactory.GetRtaCategoyDao().save(rtaCategory);
            
            rtaEvent.fkRtaCategory = rtaCategory;
            daoFactory.getRtaEventDao().save(rtaEvent);

            return rtaSettings;
        }
    }
}
