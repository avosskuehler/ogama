using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate.Cfg;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Criterion;
using System.Collections;
using NHibernate.Mapping;
using System.Data;
using OgamaDao.Model.Rta;

namespace OgamaDao.Dao
{
    public class SessionFactoryHolder
    {

        private static NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// session factory
        /// </summary>
        protected ISessionFactory sessionFactory = null;

        public ISessionFactory getHibernateSessionFactory()
        {
            return this.sessionFactory;
        }

        public void initFileBasedDatabase(string databaseFile)
        {
            Configuration config = GetSQLiteConfig();
            config.SetProperty("connection.connection_string", "Data Source=" + databaseFile + ";Version=3");
            init(config);

        }

        private void init(Configuration config)
        {
            addEntitiyMappings(config);

            sessionFactory = config.BuildSessionFactory();

            var schema = new SchemaExport(config);
            schema.Create(true, true);

            IStatelessSession s = sessionFactory.OpenStatelessSession();

            new DatabaseSchemaSqlite3().createDatabase(s);

            s.Close();
        }

        private static void addEntitiyMappings(Configuration config)
        {
            config.AddAssembly(typeof(RtaCategory).Assembly);
            //config.AddAssembly(typeof(RtaSettings).Assembly);
            //config.AddAssembly(typeof(RtaEvent).Assembly);
        }

        private Configuration GetSQLiteConfig()
        {
            Configuration config = new Configuration();
            config.SetProperty("dialect", "NHibernate.Dialect.SQLiteDialect");
            config.SetProperty("connection.provider", "NHibernate.Connection.DriverConnectionProvider");
            config.SetProperty("connection.driver_class", "NHibernate.Driver.SQLite20Driver");

            config.SetProperty("show_sql", "true");
            config.SetProperty("query.substitutions", "true=1;false=0");
            config.SetProperty("current_session_context_class", "thread_static");
            // config.SetProperty("cache.provider_class", "org.hibernate.cache.NoCacheProvider");

            return config;
        }
    }
}
