using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ogama.Modules.Rta
{
    public interface IRtaControllerListener
    {
        void onVideoFilterNameDetectionProgress(int currentItem, int maxItems, string currentItemName);
    }
}
