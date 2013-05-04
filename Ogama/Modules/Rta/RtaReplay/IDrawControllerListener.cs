﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ogama.Modules.Rta.RtaReplay
{
    public interface IDrawControllerListener
    {
        void onLeftMarkerPositionChanged(double xPositionInPercent);
        void onProgressTrackerPositionChanged(double xPositionInPercent);
        void onRightMarkerPositionChanged(double xPositionInPercent);

        String getCurrentPlayerPosition();
        String getTimeValueByXposition(double xPosition);
    }
}