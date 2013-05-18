using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ogama.Modules.Dao;
using Ogama.Modules.Rta.Model;
using Ogama.Modules.Rta.Dao;
namespace OgamaTestProject.Modules.Dao
{
    [TestClass]
    public class RtaSettingsDaoTest
    {
        [TestMethod]
        public void TestSave()
        {
            RtaSettingsDao cut = new RtaSettingsDao();
            cut.initFileBasedDatabase(BaseDaoHibernateTest.databaseFile);

            RtaSettings entity = new RtaSettings();

            long c1 = cut.count(entity);

            cut.save(entity);

            long c2 = cut.count(entity);

            Assert.AreEqual(c1 + 1, c2);
        }
    }
}
