using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ogama;
using Ogama.Modules.Database;
using OgamaDao.Dao;

namespace OgamaTestProject.Modules.Database
{
    [TestClass]
    public class DaoFactoryWrapperTest
    {
        [TestMethod]
        public void TestGetDatabaseFactoryByDocument()
        {
            Document document = new Document();
            document.ExperimentSettings = new Ogama.Properties.ExperimentSettings();
            document.ExperimentSettings.Name = "testname";
            document.ExperimentSettings.DocumentPath = TestdataProvider.GetTestFolder();
            Document.ActiveDocument = document;

            DaoFactory df = DaoFactoryWrapper.GetDaoFactory();
            Assert.IsNotNull(df);
        }
    }
}
