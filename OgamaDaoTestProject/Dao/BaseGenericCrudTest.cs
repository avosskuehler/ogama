using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OgamaDao.Dao;

namespace OgamaDaoTestProject.Dao
{
    [TestClass]
    public class BaseGenericCrudTest<T> where T : OgamaDao.Model.BaseModel
    {
        public static string databaseFile = "c:/temp/sqlite3testdb.sqlite";

        protected OgamaDao.Dao.BaseDaoHibernate<T> cut;
        
        protected T entity;

        public void Setup()
        {
            cut.initFileBasedDatabase(databaseFile);
        }

        [TestMethod]
        public void TestSave()
        {
            Guid id1 = entity.ID;
            long c1 = cut.count(entity);

            cut.save(entity);

            Guid id2 = entity.ID;
            long c2 = cut.count(entity);
            Assert.AreNotSame(id1, id2);
            Assert.AreEqual(c1 + 1, c2);

        }

        [TestMethod]
        public void TestFind()
        {
            cut.save(entity);
            IList<T> list = cut.find(entity);
            Assert.IsNotNull(list);
            Assert.IsTrue(list.Count > 0);

        }

        [TestMethod]
        public void TestDelete()
        {
            cut.save(entity);
            cut.delete(entity);

            IList<T> list = cut.find(entity);
            Assert.IsNotNull(list);
            Assert.AreEqual(list.Count, 0);
        }
    }
}