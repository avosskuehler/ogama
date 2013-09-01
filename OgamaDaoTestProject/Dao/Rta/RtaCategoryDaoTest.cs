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

            long c0 = cut.count(item1);
            cut.save(list);
            long c1 = cut.count(item1);
            Assert.AreEqual(list.Count, c1);

            foreach (RtaCategory item in list)
            {
                
            }

        }

        [TestMethod]
        public void TestSaveList()
        {
            long nItems0 = cut.count(new RtaCategory());
            int N = 3;
            List<RtaCategory> list = new List<RtaCategory>();
            for (int i = 0; i < N; i++)
            {
                RtaCategory item = new RtaCategory();
                item.name = "item:" + i;
                list.Add(item);
            }

            cut.save(list);

            long nItems1 = cut.count(new RtaCategory());
            Assert.AreNotEqual(nItems0 + N, nItems1);
            
        }

        [TestMethod]
        public void TestDeleteWithDependencies()
        {
            RtaCategory c0 = new RtaCategory();
            cut.save(c0);

            RtaCategory c1 = new RtaCategory();
            cut.save(c1);

            c0.Add(c1);
            cut.save(c0);

            Assert.IsNotNull(c1.parent);

            cut.delete(c0);
        }

        [TestMethod]
        public void TestFindAll()
        {
            cut.save(new RtaCategory());

            List<RtaCategory> list = ((RtaCategoryDao)cut).findAll();

            Assert.IsNotNull(list);
            Assert.IsTrue(list.Count > 0);
        }

        [TestMethod]
        public void TestFindByRtaSettings()
        {
            RtaCategoryDao cut = new RtaCategoryDao();
            cut.SetSessionFactory(sfh.getHibernateSessionFactory());
            RtaSettingsDao rtaSettingsDao = new RtaSettingsDao();
            rtaSettingsDao.SetSessionFactory(sfh.getHibernateSessionFactory());

            RtaSettings key = new RtaSettings();
            rtaSettingsDao.save(key);

            RtaCategory cat1 = new RtaCategory();
            cat1.fkRtaSettings = key;
            cut.save(cat1);

            List<RtaCategory> list = cut.findByRtaSettings(key);
            Assert.IsNotNull(list);
            Assert.IsTrue(list.Count > 0);
            list.ForEach(delegate (RtaCategory rtaCategory){
                Assert.AreEqual(key.ID, rtaCategory.fkRtaSettings.ID);
            });
        }
    }

    
}
