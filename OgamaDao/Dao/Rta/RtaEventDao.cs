using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgamaDao.Model.Rta;
using NHibernate;
using NHibernate.Criterion;

namespace OgamaDao.Dao.Rta
{
    public class RtaEventDao : OgamaDao.Dao.BaseDaoHibernate<RtaEvent>
    {
        protected override RtaEvent getEntity()
        {
            return new RtaEvent();
        }

        public List<RtaEvent> findByRtaCategory(RtaCategory rtaCategory)
        {
            List<RtaEvent> list = new List<RtaEvent>();


            ITransaction transaction = GetCurrentSession().Transaction;
            try
            {
                transaction.Begin();

                ISession session = GetCurrentSession();

                RtaEvent entity = new RtaEvent();
                Example example = Example.Create(entity);
                example = example.ExcludeProperty("ID");
                example = example.ExcludeZeroes();
                example = example.ExcludeNulls();

                ICriteria criteria = session.CreateCriteria(entity.GetType()).Add(example);
                criteria.Add(Restrictions.Eq("fkRtaCategory.ID", rtaCategory.ID));

                IList<RtaEvent> results = criteria.List<RtaEvent>();
                foreach (RtaEvent rtaEvent in results)
                {
                    list.Add(rtaEvent);
                }

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
          
            return list;
        }

        public List<RtaEvent> findAll()
        {
            List<RtaEvent> list = new List<RtaEvent>();
            RtaEvent pattern = new RtaEvent();
            FindRequest<RtaEvent> request = new FindRequest<RtaEvent>();
            request.entity = pattern;
            request.ignore("ID");

            IList<RtaEvent> findings = find(request);

            foreach (RtaEvent item in findings)
            {
                list.Add(item);
            }

            return list;
        }

        public override void deleteNotInList(List<RtaEvent> list)
        {

        }

        protected override void deleteChildren(RtaEvent entity, ISession session)
        {

        }
    }


}
