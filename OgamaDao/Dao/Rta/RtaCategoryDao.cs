using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgamaDao.Model.Rta;
using NHibernate;

namespace OgamaDao.Dao.Rta
{
    public class RtaCategoryDao : OgamaDao.Dao.BaseDaoHibernate<RtaCategory>
    {
        protected override RtaCategory getEntity()
        {
            return new RtaCategory();
        }

        class RtaCategoryFindByRtaSettings : HibernateCallback
        {
            private RtaSettings key;
            public RtaCategoryFindByRtaSettings(RtaSettings key)
            {
                this.key = key;
            }

            public Object doInHibernate(NHibernate.ISession session)
            {
                IQuery query = session.CreateQuery("from RtaCategory where fkRtaSettings.ID = :ID1");
                query.SetGuid("ID1", key.ID);
                return query.List<RtaCategory>();
            }
        }

        public List<RtaCategory> findByRtaSettings(RtaSettings rtaSettings)
        {
            List<RtaCategory> list = new List<RtaCategory>();

            Object result = base.doInSession(new RtaCategoryFindByRtaSettings(rtaSettings));
            if (result is List<RtaCategory>)
            {
                list = (List<RtaCategory>)result;
            }
            return list;
        }

        public List<RtaCategory> findAll()
        {
            List<RtaCategory> list = new List<RtaCategory>();
            RtaCategory pattern = new RtaCategory();
            FindRequest<RtaCategory> request = new FindRequest<RtaCategory>();
            request.entity = new RtaCategory();
            request.ignore("ID");
            request.ignore("show");

            IList<RtaCategory> findings = find(request);

            foreach (RtaCategory item in findings)
            {
                list.Add(item);
            }

            return list;
        }


        public override void deleteNotInList(List<RtaCategory> list)
        {
            List<RtaCategory> existingItems = findAll();
            List<RtaCategory> toRemove = new List<RtaCategory>();
            foreach (RtaCategory existingItem in existingItems)
            {
                bool remove = true;
                foreach (RtaCategory newItem in list)
                {
                    if (existingItem.ID.Equals(newItem.ID))
                    {
                        remove = false;
                        break;
                    }
                }
                if (remove)
                {
                    toRemove.Add(existingItem);
                }
            }

            foreach (RtaCategory item in toRemove)
            {
                try
                {
                    delete(item);
                }
                catch (Exception e)
                {
                    log.Error(e);
                }
            }
        }

        protected override void deleteChildren(RtaCategory entity, ISession session)
        {
            IQuery query = session.CreateQuery("delete from RtaEvent where fkRtaCategory.ID = :ID1");
            query = query.SetGuid("ID1", entity.ID);
            int deleted = query.ExecuteUpdate();
            log.Info("delete dependencies: " + deleted);
        }
        
    }
}
