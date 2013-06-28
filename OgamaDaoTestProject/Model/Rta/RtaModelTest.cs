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
        private TestdataProvider testdataProvider = new TestdataProvider();
        
        [TestInitialize]
        public void setUp()
        {
            cut = new RtaModel();
            daoFactory.init(TestdataProvider.getTestDatabaseFilename());
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
        public void TestAddWrongRtaEvent()
        {

            RtaEvent newRtaEvent = new RtaEvent();

            try
            {
                cut.Add(newRtaEvent);
                Assert.Fail();
            }
            catch (System.InvalidOperationException e)
            {
                Assert.IsNotNull(e);
            }
            
        }

        [TestMethod]
        public void TestAddRtaEvent()
        {
            RtaSettings rtaSettings = testdataProvider.createTestModel(daoFactory);
            RtaCategory rtaCategory = new RtaCategory();
            RtaEvent rtaEvent = new RtaEvent();
            rtaEvent.fkRtaCategory = rtaCategory;
           
            cut.Add(rtaEvent);

        }

        [TestMethod]
        public void TestRemoveRtaEvent()
        {
            RtaCategory rtaCategory = new RtaCategory();
            RtaEvent rtaEvent = new RtaEvent();
            rtaEvent.fkRtaCategory = rtaCategory;
            cut.SetRtaCategoryDao(daoFactory.GetRtaCategoyDao());
            cut.SetRtaEventDao(daoFactory.getRtaEventDao());
            cut.Add(rtaEvent);
            
            cut.Remove(rtaEvent);

            Assert.AreEqual(0,rtaCategory.GetRtaEvents().Count);
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
            RtaSettings rtaSettings = testdataProvider.createTestModel(daoFactory);

            cut.SetRtaEventDao(daoFactory.getRtaEventDao());
            cut.SetRtaCategoryDao(daoFactory.GetRtaCategoyDao());

            Assert.AreEqual(0,cut.getRtaCategories().Count);

            cut.Load(rtaSettings);

            RtaCategory rtaCategory = cut.getRtaCategories().ElementAt(0);
            Assert.IsNotNull(rtaCategory);
            RtaEvent rtaEvent = rtaCategory.GetRtaEvents().ElementAt(0);
            Assert.IsNotNull(rtaEvent);
            
        }

        [TestMethod]
        public void TestSave()
        {
            cut = new RtaModel();
            cut.Init(daoFactory);

            Assert.AreEqual(0, cut.getRtaCategories().Count);

            RtaSettings rtaSettings = testdataProvider.createTestModel(daoFactory);
            
            Assert.AreEqual(0, cut.getRtaCategories().Count);

            cut.Load(rtaSettings);

            Assert.AreEqual(1, cut.getRtaCategories().Count);
            
            cut.Save();
            Assert.AreEqual(1, cut.getRtaCategories().Count);
            
            cut.Load(rtaSettings);
            Assert.AreEqual(1, cut.getRtaCategories().Count);
            

        }

        [TestMethod]
        public void TestChangeModelAndRevert()
        {
            //fetch the model
            cut = new RtaModel();
            cut.Init(daoFactory);
            RtaSettings rtaSettings = testdataProvider.createTestModel(daoFactory);

            cut.Load(rtaSettings);
            Assert.AreEqual(1, cut.getRtaCategories().Count);

            RtaCategory newRtaCategory = new RtaCategory();
            cut.Add(newRtaCategory);
            Assert.AreEqual(2, cut.getRtaCategories().Count);

            cut.Load(rtaSettings);
            Assert.AreEqual(1, cut.getRtaCategories().Count);
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
