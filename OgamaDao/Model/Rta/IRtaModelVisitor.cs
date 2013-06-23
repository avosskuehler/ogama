using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OgamaDao.Model.Rta
{
    public interface IRtaModelVisitor
    {
        void onVisit(RtaCategory rtaCategory);

        void onVisit(RtaEvent rtaEvent);

    }
}
