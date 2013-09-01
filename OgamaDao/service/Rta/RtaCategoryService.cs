using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgamaDao.Dao.Rta;
namespace OgamaDao.service.Rta
{
    public class RtaCategoryService
    {
        private RtaCategoryDao RtaCategoryDao;
        
        public void SetRtaCategoryDao(RtaCategoryDao dao)
        {
            this.RtaCategoryDao = dao;
        }

        
    }
}
