using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OgamaDao.Dao;
using OgamaDao.Dao.Rta;

namespace OgamaDaoTestProject.Dao
{
    [TestClass]
    public class DaoFactoryTest
    {
        NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        DaoFactory cut = new DaoFactory();
        string databaseFilename = System.IO.Path.GetTempFileName();

        [TestInitialize]
        public void SetUp()
        {
            cut = new DaoFactory();
            databaseFilename = "c:/temp/testdb.sqlite3";
            cut.init(databaseFilename);
        }

        
        [TestMethod]
        public void TestGetRtaCategoryDao()
        {
            RtaCategoryDao dao = cut.GetRtaCategoyDao();
            Assert.IsNotNull(dao);
            RtaCategoryDao dao2 = cut.GetRtaCategoyDao();
            Assert.AreEqual(dao,dao2);
        }
    }
}
