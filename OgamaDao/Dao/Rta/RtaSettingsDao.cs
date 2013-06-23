using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgamaDao.Model.Rta;
using OgamaDao.Dao;

namespace OgamaDao.Dao.Rta
{
    public class RtaSettingsDao : OgamaDao.Dao.BaseDaoHibernate<RtaSettings>
    {
        
        protected override RtaSettings getEntity()
        {
            return new RtaSettings();
        }

        public override void deleteNotInList(List<RtaSettings> list)
        {
        }
    }
}


