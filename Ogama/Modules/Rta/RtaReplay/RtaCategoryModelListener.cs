using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ogama.Modules.Rta.RtaReplay
{
    interface RtaCategoryModelListener
    {
        void onEventAdded(RtaEvent rtaEvent);

        void onEventChanged(RtaEvent rtaEvent);
        
        void onEventRemoved(RtaEvent rtaEvent);

        void onCategoryAdded(RtaCategory rtaCategory);

        void onCategoryChanged(RtaCategory rtaCategory);

        void onCategoryRemoved(RtaCategory rtaCategory);

    }
}
