using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ogama.Modules.Rta.RtaReplay
{
    public class RtaPanelController
    {
        RtaPanel activeRtaPanel;

        public void setActiveRtaPanel(RtaPanel rtaPanel)
        {
            this.activeRtaPanel = rtaPanel;
        }



        public void onSpaceKey()
        {
            if (this.activeRtaPanel == null)
            {
                return;
            }
            this.activeRtaPanel.onSpaceKey();
        }
    }
}
