using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OgamaDao.Dao
{
    public interface HibernateCallback
    {
        Object doInHibernate(NHibernate.ISession session);

    }
}
