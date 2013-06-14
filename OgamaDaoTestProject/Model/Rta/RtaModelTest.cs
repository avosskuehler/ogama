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
        private RtaEvent e1 = new RtaEvent();

        [TestInitialize]
        public void setUp()
        {
            cut = new RtaModel();
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
            cut.Add(e1);
            int s1 = cut.getRtaEvents().Count;
            Assert.AreEqual(s0 + 1, s1);

        }

        [TestMethod]
        public void TestRemoveRtaEvent()
        {
            cut.Add(e1);
            Assert.AreEqual(1, cut.getRtaEvents().Count);
            cut.Remove(e1);
            Assert.AreEqual(0, cut.getRtaEvents().Count);
        }

        [TestMethod]
        public void TestSave()
        {
            DaoFactory daoFactory = new DaoFactory();
            daoFactory.init("c:/temp/test.db");
            RtaCategoryDao rtaCategoryDao = daoFactory.GetRtaCategoyDao();
            cut.SetRtaCategoryDao(rtaCategoryDao);

            cut.SaveRtaCategories();

        }
    }
}
