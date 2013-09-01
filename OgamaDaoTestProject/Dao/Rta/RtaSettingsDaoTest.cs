using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OgamaDao.Model.Rta;
using OgamaDao.Dao.Rta;

namespace OgamaDaoTestProject.Dao.Rta
{
    [TestClass]
    public class RtaSettingsDaoTest : BaseGenericCrudTest<RtaSettings>
    {
        [TestInitialize]
        public void init()
        {
            base.entity = new RtaSettings();
            base.cut = new RtaSettingsDao();
            base.Setup();
        }
    }
}