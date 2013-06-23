using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgamaDao.Model.Rta;

namespace OgamaDao.Dao.Rta
{
    public class RtaCategoryDao : OgamaDao.Dao.BaseDaoHibernate<RtaCategory>
    {
        private NLog.Logger log = new NLog.LogFactory().GetCurrentClassLogger();

        protected override RtaCategory getEntity()
        {
            return new RtaCategory();
        }

        public List<RtaCategory> findAll()
        {
            List<RtaCategory> list = new List<RtaCategory>();
            RtaCategory pattern = new RtaCategory();
            FindRequest<RtaCategory> request = new FindRequest<RtaCategory>();
            request.entity = new RtaCategory();
            request.ignore("ID");
            request.ignore("show");
            request.page = 0;
            request.pageSize = int.MaxValue;

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
        
    }
}
