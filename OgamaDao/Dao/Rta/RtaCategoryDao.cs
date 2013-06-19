using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgamaDao.Model.Rta;

namespace OgamaDao.Dao.Rta
{
    public class RtaCategoryDao : OgamaDao.Dao.BaseDaoHibernate<RtaCategory>
    {

        public List<RtaCategory> findAll()
        {
            List<RtaCategory> list = new List<RtaCategory>();
            RtaCategory pattern = new RtaCategory();
            FindRequest<RtaCategory> request = new FindRequest<RtaCategory>();
            request.entity = new RtaCategory();
            request.ignore("ID");

            IList<RtaCategory> findings = find(request);

            foreach (RtaCategory item in findings)
            {
                list.Add(item);
            }

            return list;
        }

      
        
    }
}
