using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ogama.Modules.Dao;
using Ogama.Modules.Rta.Model;
namespace OgamaTestProject.Modules.Dao
{
    [TestClass]
    public class BaseDaoHibernateTest
    {
        private BaseDaoHibernate<RtaSettings> cut = new BaseDaoHibernate<RtaSettings>();        
        public static string databaseFile = "c:/temp/sqlite3testdb.sqlite";
       
        
        [TestInitialize]
        public void setUp()
        {
            cut.initFileBasedDatabase(databaseFile);
            //cut.initInMemoryDatabase();
        }

        [TestMethod]
        public void TestSave()
        {
        
            RtaSettings entity = new RtaSettings();
            Guid id1 = entity.ID;
            cut.save(entity);
            Guid id2 = entity.ID;
            Assert.IsFalse(id1.Equals(id2));
            
        }

        [TestMethod]
        public void TestFind()
        {
            
            RtaSettings entity = new RtaSettings();

            cut.save(entity);

            
            FindRequest<RtaSettings> request = new FindRequest<RtaSettings>();
            request.entity = entity;
            request.page = 0;
            request.pageSize = 10;
            IList<RtaSettings> list = cut.find(request);
            System.Diagnostics.Debug.WriteLine(":-)");

            Assert.IsNotNull(list);
            Assert.IsTrue(list.Count > 0);

        }

        [TestMethod]
        public void TestDelete()
        {
            RtaSettings entity = new RtaSettings();

            cut.save(entity);

            IList<RtaSettings> list = cut.find(entity);

            RtaSettings entity2 = list.ElementAt(0);
            
            cut.delete(entity2);

        }
    }
}
