using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OgamaDao.Dao.Rta;
using OgamaDao.Model.Rta;
using OgamaDao.Dao;

namespace OgamaDaoTestProject.Dao.Rta
{
    [TestClass]
    public class RtaEventDaoTest : BaseGenericCrudTest<RtaEvent>
    {
        [TestInitialize]
        public void Init()
        {
            base.entity = new RtaEvent();
            base.cut = new RtaEventDao();
            base.Setup();
        }

        [TestMethod]
        public void TestUpdateEventWithCategory()
        {
            
            
            RtaCategory category1 = new RtaCategory();
            RtaCategoryDao categoryDao = GetRtaCategoryDao();
            categoryDao.save(category1);

            RtaEvent event1 = new RtaEvent();
            
            event1.fkRtaCategory = category1;
            RtaEventDao rtaEventDao = new RtaEventDao();
            rtaEventDao.SetSessionFactory(base.sfh.getHibernateSessionFactory());
            rtaEventDao.save(event1);

            RtaEvent event2 = rtaEventDao.findById(event1);
            Assert.IsNotNull(event2);
            Assert.AreEqual(event1.ID, event2.ID);

        }

        private RtaCategoryDao GetRtaCategoryDao()
        {
            RtaCategoryDao categoryDao = new RtaCategoryDao();
            categoryDao.SetSessionFactory(base.sfh.getHibernateSessionFactory());
            return categoryDao;
        }

        [TestMethod]
        public void TestFindByRtaCategory()
        {
            RtaCategoryDao rtaCategoryDao = GetRtaCategoryDao();
            RtaEventDao cut = GetRtaEventDao();

            RtaCategory cat1 = new RtaCategory();
            rtaCategoryDao.save(cat1);

            RtaEvent rtaEvent = new RtaEvent();
            rtaEvent.fkRtaCategory = cat1;
            cut.save(rtaEvent);

            List<RtaEvent> list = cut.findByRtaCategory(cat1);

            Assert.IsNotNull(list);
            Assert.IsTrue(list.Count > 0);
            foreach (RtaEvent rtaEventFound in list)
            {
                Assert.AreEqual(rtaEventFound.fkRtaCategory.ID, cat1.ID);
            }
        }

        private RtaEventDao GetRtaEventDao()
        {
            RtaEventDao dao = new RtaEventDao();
            dao.SetSessionFactory(base.sfh.getHibernateSessionFactory());
            return dao;
        }


        [TestMethod]
        public void TestFindAll()
        {
            cut.save(new RtaEvent());
            List<RtaEvent> list = ((RtaEventDao)cut).findAll();
            Assert.IsNotNull(list);
            Assert.IsTrue(list.Count > 0);
        }
    }
}
