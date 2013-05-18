using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ogama.Modules.Rta.Model;
using Ogama.Modules.Rta.Dao;

namespace OgamaTestProject.Modules.Rta.Dao
{
    [TestClass]
    public class RtaEventDaoTest : BaseGenericCrudTest<RtaEvent>
    {

        [TestInitialize]
        public void Setup()
        {
            base.entity = new RtaEvent();
            base.cut = new RtaEventDao();
            base.Setup();
        }
    }
}
