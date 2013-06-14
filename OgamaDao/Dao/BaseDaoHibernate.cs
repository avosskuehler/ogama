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

using OgamaDao.Model;
using OgamaDao.Model.Rta;

namespace OgamaDao.Dao
{
    /// <summary>
    /// base generic dao
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseDaoHibernate<T> where T : BaseModel
    {
        private static NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// session factory
        /// </summary>
        protected ISessionFactory sessionFactory = null;


        
        /// <summary>
        /// 
        /// initialize hibernate session factory
        /// </summary>
        public void initFileBasedDatabase(string databaseFile)
        {
            Configuration config = GetSQLiteConfig();
            config.SetProperty("connection.connection_string", "Data Source=" + databaseFile + ";Version=3");
            init(config);
        }
        /// <summary>
        /// 
        /// 
        /// </summary>
        public void initInMemoryDatabase()
        {
            Configuration config = GetSQLiteConfig();
            config.SetProperty("connection.connection_string", "Data Source=:memory:");
            
            addEntitiyMappings(config);

            this.sessionFactory = config.BuildSessionFactory();

            
            var schema = new SchemaExport(config);
            IDbConnection connection = GetCurrentSession().Connection;

            //schema.Execute(true, true, false, connection, System.Diagnostics.Trace);
            schema.Create(true, true);
        }

        private void init(Configuration config)
        {
            addEntitiyMappings(config);

            this.sessionFactory = config.BuildSessionFactory();

            var schema = new SchemaExport(config);
            schema.Create(true, true);

            IStatelessSession s = this.sessionFactory.OpenStatelessSession();

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

        
        private void initFilebasedConnection(Configuration config, string databaseFile)
        {
            config.SetProperty("dialect", "NHibernate.Dialect.SQLiteDialect");
            config.SetProperty("connection.provider", "NHibernate.Connection.DriverConnectionProvider");
            config.SetProperty("connection.driver_class", "NHibernate.Driver.SQLite20Driver");
            config.SetProperty("connection.connection_string", "Data Source=" + databaseFile + ";Version=3");
            
            config.SetProperty("show_sql", "true");
            config.SetProperty("query.substitutions", "true=1;false=0");
            config.SetProperty("current_session_context_class", "thread_static");
        }

        /// <summary>
        /// get or open a hibernate session
        /// </summary>
        /// <returns></returns>

        protected ISession GetCurrentSession()
        {
            if (sessionFactory == null)
            {
                log.Error("no session factory");
            }
            if (!NHibernate.Context.ThreadStaticSessionContext.HasBind(sessionFactory))
            {
                NHibernate.Context.ThreadStaticSessionContext.Bind(sessionFactory.OpenSession());
            } 
            
            return this.sessionFactory.GetCurrentSession();
        }

        /// <summary>
        /// close the current hibernate session
        /// </summary>
        protected void DisposeCurrentSession()
        {
            ISession currentSession = NHibernate.Context.ThreadStaticSessionContext.Unbind(this.sessionFactory);
            currentSession.Flush();
            currentSession.Close();
            currentSession.Dispose();
        }

        public long count(T entity)
        {
            ISession session = this.GetCurrentSession();
            try
            {
               IQuery query = session.CreateQuery("select count (*) from "+entity.GetType().Name);
               string s = query.QueryString;
               log.Info("query:" + s);

               long count = 0;

               count = (long)query.UniqueResult();
               
               return count;
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
            finally
            {
                this.DisposeCurrentSession();
            }
        }

        /// <summary>
        /// write the given entity into the database
        /// </summary>
        /// <param name="entity"></param>
        public void save(T entity)
        {
            ISession session = this.GetCurrentSession();
            try
            {
                object id = session.Save(entity);
                entity.ToString();
                entity.ID = (Guid)id;
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
            finally
            {
                this.DisposeCurrentSession();
            }
            
        }

        public T findById(T entity)
        {
            ITransaction transaction = GetCurrentSession().Transaction;
            try
            {
                transaction.Begin();

                ISession session = GetCurrentSession();
                
                Type t = typeof(RtaSettings);
                T result = session.CreateCriteria(entity.GetType())
                    .Add(Example.Create(entity))
                    .UniqueResult<T>();

                return result;

            }
            catch (Exception e)
            {
                transaction.Rollback();
                log.Error(e);
                throw e;
            }
            finally
            {
                transaction.Commit();
            }
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IList<T> find(T entity)
        {
            FindRequest<T> request = new FindRequest<T>();
            request.entity = entity;
            request.page = 0;
            request.pageSize = Int16.MaxValue;

            return find(request);
        }

        /// <summary>
        /// 
        /// find entity by given pattern
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IList<T> find(FindRequest<T> request)
        {
            ITransaction transaction = GetCurrentSession().Transaction;
            try
            {
                transaction.Begin();

                ISession session = GetCurrentSession();
                T entity = request.entity;
                Type t = typeof(RtaSettings);
                IList<T> results = session.CreateCriteria(entity.GetType())
                    .Add(Example.Create(entity))
                    .List<T>();
                
                return results;

            }
            catch (Exception e)
            {
                transaction.Rollback();
                log.Error(e);
                throw e;
            }
            finally
            {
                transaction.Commit();
            }
            
        }

        public void delete(T entity)
        {
            ITransaction transaction = GetCurrentSession().Transaction;
            try
            {
                transaction.Begin();

                ISession session = GetCurrentSession();

                session.Delete(entity);

            }
            catch (Exception e)
            {
                transaction.Rollback();
                log.Error(e);
                throw e;
            }
            finally
            {
                transaction.Commit();
            }
        }

    }
}
