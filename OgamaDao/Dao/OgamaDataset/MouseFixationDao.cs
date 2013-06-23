using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgamaDao;
using OgamaDao.Dao;
using OgamaDao.Model;
using OgamaDao.Model.OgamaDataset;

namespace OgamaDao.Dao.OgamaDataset
{
    public class MouseFixationDao : BaseDaoHibernate<OgamaDao.Model.OgamaDataset.MouseFixation>
    {
        protected override Model.OgamaDataset.MouseFixation getEntity()
        {
            return new Model.OgamaDataset.MouseFixation();
        }

        public override void deleteNotInList(List<MouseFixation> list)
        {

        }
    }
}
