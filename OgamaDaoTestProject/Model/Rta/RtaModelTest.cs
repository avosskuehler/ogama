using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OgamaDao.Model.Rta;
using OgamaDao.Dao;
using OgamaDao.Dao.Rta;

namespace OgamaDaoTestProject.Model.Rta
{
    [TestClass]
    public class RtaModelTest
    {
        private RtaModel cut;
        private RtaCategory c1 = new RtaCategory();
        private RtaCategory rtaCategory1 = new RtaCategory();
        private RtaEvent rtaEvent1 = new RtaEvent();
        private DaoFactory daoFactory = new DaoFactory();

        [TestInitialize]
        public void setUp()
        {
            cut = new RtaModel();
            daoFactory.init("c:/temp/test.db");
        }

        [TestMethod]
        public void TestAddRtaCategory()
        {
            Assert.IsNotNull(cut);
            
            int s0 = cut.getRtaCategories().Count;
            cut.Add(c1);
            int s1 = cut.getRtaCategories().Count;
            Assert.AreEqual(s0 + 1, s1);

        }

        [TestMethod]
        public void TestRemoveRtaCategory()
        {
            cut.Add(c1);
            Assert.AreEqual(1, cut.getRtaCategories().Count);
            cut.Remove(c1);
            Assert.AreEqual(0, cut.getRtaCategories().Count);

        }

        [TestMethod]
        public void TestAddRtaEvent()
        {
            
            int s0 = cut.getRtaEvents().Count;
            cut.Add(rtaEvent1);
            int s1 = cut.getRtaEvents().Count;
            Assert.AreEqual(s0 + 1, s1);

        }

        [TestMethod]
        public void TestRemoveRtaEvent()
        {
            cut.Add(rtaEvent1);
            Assert.AreEqual(1, cut.getRtaEvents().Count);
            cut.Remove(rtaEvent1);
            Assert.AreEqual(0, cut.getRtaEvents().Count);
        }

        [TestMethod]
        public void TestSaveRtaCategories()
        {
            
            
            RtaCategoryDao rtaCategoryDao = daoFactory.GetRtaCategoyDao();
            cut.SetRtaCategoryDao(rtaCategoryDao);
           
            long c0 = cut.getRtaCategories().Count();
            cut.SaveRtaCategories();
            long c1 = rtaCategoryDao.count(rtaCategory1);

            Assert.AreEqual(c0, c1);

        }

        [TestMethod]
        public void TestSaveRtaEvents()
        {
            RtaEventDao rtaEventDao = daoFactory.getRtaEventDao();
            cut.SetRtaEventDao(rtaEventDao);
            long c0 = cut.getRtaEvents().Count();
            Guid id0 = rtaEvent1.ID;
            
            cut.Add(rtaEvent1);
           
            cut.SaveRtaEvents();

            Guid id1 = rtaEvent1.ID;
            Assert.AreNotEqual(id0, id1);

            long c1 = rtaEventDao.count(rtaEvent1);
            Assert.AreEqual(c0 + 1, c1);
        }

        [TestMethod]
        public void TestGetRtaCategories()
        {
            List<RtaCategory> list = cut.getRtaCategories();
            Assert.IsNotNull(list);
            long c0 = list.Count();

            cut.Add(rtaCategory1);

            list = cut.getRtaCategories();
            long c1 = list.Count();
            Assert.AreEqual(c0 + 1, c1);
        }

        [TestMethod]
        public void TestLoad()
        {
            RtaCategoryDao rtaCategoryDao = daoFactory.GetRtaCategoyDao();
            rtaCategoryDao.save(rtaCategory1);
            Assert.AreEqual(1, rtaCategoryDao.count(new RtaCategory()));

            RtaEventDao rtaEventDao = daoFactory.getRtaEventDao();
            rtaEventDao.save(rtaEvent1);
            Assert.AreEqual(1, rtaCategoryDao.count(new RtaCategory()));

            cut.SetRtaEventDao(rtaEventDao);
            cut.SetRtaCategoryDao(rtaCategoryDao);

            long numberOfrtaCategories0 = cut.getRtaCategories().Count();
            Assert.AreEqual(0, numberOfrtaCategories0);
            
            cut.Load();
            
            long numberOfRtaCategories1 = cut.getRtaCategories().Count();
            Assert.AreEqual(1, numberOfRtaCategories1);

            long numberOfRtaEvents1 = cut.getRtaEvents().Count();
            Assert.AreEqual(1, numberOfRtaEvents1);
        }

        [TestMethod]
        public void TestSave()
        {
            cut = new RtaModel();

            Assert.AreEqual(0, cut.getRtaCategories().Count());
            Assert.AreEqual(0, cut.getRtaEvents().Count());

            
            cut.SetRtaCategoryDao(daoFactory.GetRtaCategoyDao());
            cut.SetRtaEventDao(daoFactory.getRtaEventDao());

            cut.Add(new RtaCategory());
            cut.Add(new RtaEvent());

            cut.Save();

            cut = new RtaModel();
            cut.SetRtaCategoryDao(daoFactory.GetRtaCategoyDao());
            cut.SetRtaEventDao(daoFactory.getRtaEventDao());
            cut.Load();

            Assert.AreEqual(1, cut.getRtaCategories().Count());
            Assert.AreEqual(1, cut.getRtaEvents().Count());
        }

        [TestMethod]
        public void TestChangeModelAndRevert()
        {
            //fetch the model
            cut = new RtaModel();
            cut.SetRtaCategoryDao(daoFactory.GetRtaCategoyDao());
            cut.SetRtaEventDao(daoFactory.getRtaEventDao());
            int numberOfCategories = cut.getRtaCategories().Count;
            Assert.AreEqual(0, numberOfCategories);
            //add some stuff
            cut.Add(new RtaCategory());
            cut.Add(new RtaEvent());

            //now don't save the model
            cut.Load();

            //then: the model shall remain unchanged
            Assert.AreEqual(0, numberOfCategories);

        }

        [TestMethod]
        public void TestVisit()
        {
            ModelVisitor visitor = new ModelVisitor();
            RtaCategory c1 = new RtaCategory();
            c1.name = "c1";
            RtaCategory c2 = new RtaCategory();
            c2.name = "c2";
            c1.Add(c2);
            cut.getRtaCategories().Add(c1);
            RtaCategory c3 = new RtaCategory();
            c3.name = "c3";
            cut.getRtaCategories().Add(c3);

            cut.visit(visitor);

            Assert.AreEqual(3, visitor.rtaCategories.Count);
        }
    }

    class ModelVisitor : IRtaModelVisitor
    {
        public List<RtaCategory> rtaCategories = new List<RtaCategory>();
        public void onVisit(RtaCategory rtaCategory)
        {
            rtaCategories.Add(rtaCategory);
        }

        public void onVisit(RtaEvent rtaEvent)
        {

        }
    }
}
