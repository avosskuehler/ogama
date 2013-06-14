using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgamaDao.Model.Rta;

namespace OgamaDao.Dao.Rta
{
    public class RtaCategoryDao : OgamaDao.Dao.BaseDaoHibernate<RtaCategory>
    {

        public void save(List<RtaCategory> list)
        {

            foreach( RtaCategory item in list)
            {
                base.save(item);
            }
        }
    }
}
