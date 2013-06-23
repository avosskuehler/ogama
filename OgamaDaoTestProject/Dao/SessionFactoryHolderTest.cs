using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OgamaDao.Dao;

namespace OgamaDaoTestProject.Dao
{
    [TestClass]
    public class SessionFactoryHolderTest
    {
        [TestMethod]
        public void TestCreateSchema()
        {
            SessionFactoryHolder cut = new SessionFactoryHolder();
            cut.SetCreateSchema(false);
            cut.initFileBasedDatabase(TestdataProvider.getTestDatabaseFilename());

        }
    }
}
