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
    public abstract class BaseDaoHibernate<T> where T : BaseModel
    {
        protected static NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// session factory
        /// </summary>
        protected ISessionFactory sessionFactory;
        public void SetSessionFactory(ISessionFactory sf)
        {
            this.sessionFactory = sf;
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
            
            return sessionFactory.GetCurrentSession();
        }

        /// <summary>
        /// close the current hibernate session
        /// </summary>
        protected void DisposeCurrentSession()
        {
            ISession currentSession = NHibernate.Context.ThreadStaticSessionContext.Unbind(sessionFactory);
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

        protected abstract T getEntity();

        public abstract void deleteNotInList(List<T> list);

        public void save(List<T> list)
        {
            
            this.deleteNotInList(list);

            ISession session = this.GetCurrentSession();
            ITransaction ta = session.BeginTransaction();
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    T entity = list.ElementAt(i);
                    if (entity.ID.Equals(Guid.Empty))
                    {
                        Guid newID = (Guid)session.Save(entity);
                        entity.ID = newID;
                    }
                    else
                    {
                        entity = session.Merge(entity);
                    }
                }
                ta.Commit();
                this.DisposeCurrentSession();
            }
            catch (Exception e)
            {
                ta.Rollback();
                log.Error(e);
            }
            finally
            {
                
            }
        }

        /// <summary>
        /// write the given key into the database
        /// </summary>
        /// <param name="key"></param>
        public void save(T entity)
        {
            ISession session = this.GetCurrentSession();
            try
            {
                if (entity.ID.Equals(Guid.Empty))
                {
                    Guid newID = (Guid)session.Save(entity);
                    entity.ID = newID;
                }
                else
                {
                    T entityUpdated = session.Merge(entity);

                }
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
                string entityName = entity.GetType().Name;
                T result = (T)session.Get(entityName, entity.ID); 
                
                transaction.Commit();

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
                
            }
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IList<T> find(T entity)
        {
            FindRequest<T> request = new FindRequest<T>();
            request.entity = entity;
           
            if (entity.ID.Equals(Guid.Empty))
            {
                request.ignore("ID");
            }
            return find(request);
        }


        public Object doInSession(HibernateCallback callback)
        {
            Object result = null;
            ITransaction transaction = GetCurrentSession().Transaction;
            try
            {
                transaction.Begin();

                ISession session = GetCurrentSession();

                result = callback.doInHibernate(session);
            }
            catch (Exception e)
            {
                transaction.Rollback();
                log.Error(e);
                throw e;
            }
            finally
            {

            }
            return result;
        }

        /// <summary>
        /// 
        /// find key by given pattern
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IList<T> find(FindRequest<T> request)
        {
            ITransaction transaction = GetCurrentSession().Transaction;
            try
            {
                transaction.Begin();

                ISession session = GetCurrentSession();
                
                T entity = request.entity;
                
                Example example = Example.Create(entity);
                foreach (string p in request.ignoreProperties)
                {
                    example = example.ExcludeProperty(p);
                }
                example = example.ExcludeZeroes();
                example = example.ExcludeNulls();
                
                ICriteria criteria = session.CreateCriteria(entity.GetType()).Add(example);

                if (request.pageSize > 0)
                {
                    criteria = criteria.SetFirstResult(request.page * request.pageSize);
                    criteria = criteria.SetFetchSize(request.pageSize);
                }
                
                IList<T> results = criteria.List<T>();

                transaction.Commit();

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
                
            }
            
        }

        public void delete(T entity)
        {
            ITransaction transaction = GetCurrentSession().Transaction;
            try
            {
                transaction.Begin();

                ISession session = GetCurrentSession();

                deleteChildren(entity, session);

                session.Delete(entity);

                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                log.Error(e);
                throw e;
            }
            finally
            {
                
            }
        }


        protected abstract void deleteChildren(T entity, ISession session);
        
        
    }
}
