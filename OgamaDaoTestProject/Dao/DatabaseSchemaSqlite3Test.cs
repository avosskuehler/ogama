using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Criterion;

using OgamaDao.Dao;

namespace OgamaDaoTestProject.Dao
{
    [TestClass]
    public class DatabaseSchemaSqlite3Test
    {
        [TestMethod]
        public void TestCreateDatabase()
        {
            IStatelessSession session = getSession();

            DatabaseSchemaSqlite3 cut = new DatabaseSchemaSqlite3();

            cut.createDatabase(session);

            session.Close();

        }

        private static IStatelessSession getSession()
        {
            string databaseFile = TestdataProvider.getTestDatabaseFilename();
            Configuration config = new Configuration();
            config.SetProperty("dialect", "NHibernate.Dialect.SQLiteDialect");
            config.SetProperty("connection.provider", "NHibernate.Connection.DriverConnectionProvider");
            config.SetProperty("connection.driver_class", "NHibernate.Driver.SQLite20Driver");

            config.SetProperty("show_sql", "true");
            config.SetProperty("query.substitutions", "true=1;false=0");
            config.SetProperty("current_session_context_class", "thread_static");
            config.SetProperty("connection.connection_string", "Data Source=" + databaseFile + ";Version=3");

            ISessionFactory sessionFactory = config.BuildSessionFactory();
            IStatelessSession session = sessionFactory.OpenStatelessSession();
            return session;
        }
    }
}
