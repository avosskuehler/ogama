using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ogama.Modules.Rta.Model;
using Ogama.Modules.Rta.Dao;
using OgamaTestProject.Modules.Dao;

namespace OgamaTestProject.Modules.Rta.Dao
{
    [TestClass]
    public class RtaSettingsDaoTest : BaseGenericCrudTest<RtaSettings>
    {
        
        [TestInitialize]
        public void Setup()
        {
            base.entity = new RtaSettings();
            base.cut = new RtaSettingsDao();
            cut.initFileBasedDatabase(BaseDaoHibernateTest.databaseFile);
        }

     
    }
}
