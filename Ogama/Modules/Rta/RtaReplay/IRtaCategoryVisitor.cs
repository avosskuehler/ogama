using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ogama.Modules.Rta.RtaReplay
{
    public interface IRtaCategoryVisitor
    {
        void onVisit(RtaCategory rtaCategory);

        void onVisit(RtaEvent rtaEvent);
    }
}
