using OgamaDao.Dao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OgamaDao.Dao.Rta;
using OgamaDao.Model.Rta;
using System.Collections.Generic;

namespace OgamaDaoTestProject.Dao.Rta
{
    
    
    /// <summary>
    ///Dies ist eine Testklasse für "RtaCategoryDaoTest" und soll
    ///alle RtaCategoryDaoTest Komponententests enthalten.
    ///</summary>
    [TestClass]
    public class RtaCategoryDaoTest : BaseGenericCrudTest<RtaCategory>
    {
        /// <summary>
        /// init crud test
        /// </summary>
        [TestInitialize]
        public void init()
        {
            base.entity = new RtaCategory();
            base.cut = new RtaCategoryDao();
            base.Setup();
        }

        [TestMethod]
        public void TestSaveAll()
        {
            List<RtaCategory> list = new List<RtaCategory>();
            RtaCategory item1 = new RtaCategory();
            item1.name = "item1";
            list.Add(item1);

            RtaCategory item2 = new RtaCategory();
            item1.Add(item2);

            RtaCategoryDao cut = new RtaCategoryDao();
            cut.initFileBasedDatabase(databaseFile);

            long c0 = cut.count(item1);
            cut.save(list);
            long c1 = cut.count(item1);
            Assert.AreEqual(c0 + 2, c1);

            foreach (RtaCategory item in list)
            {
                
            }

        }
       
    }

    
}
