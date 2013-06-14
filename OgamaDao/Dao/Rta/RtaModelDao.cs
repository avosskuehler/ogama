using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgamaDao.Model.Rta;


namespace OgamaDao.Dao.Rta
{
    public class RtaModelDao
    {
        
        public void Add(RtaCategory newCategory, RtaCategory parent)
        {
            newCategory.parent = parent;
        }
    }
}
