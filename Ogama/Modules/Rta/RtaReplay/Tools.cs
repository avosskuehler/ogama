using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ogama.Modules.Rta.RtaReplay
{
    public class Tools
    {
        public double intValue2Percent(int value, int max)
        {
            double dH = (double)100;
            double dMax = (double)max;
            double dValue = (double) value;
            return (dH / dMax ) * dValue;
        }

        public int percentValue2Int(double value, int max)
        {
            double dH = (double)100;
            double dMax = (double)max;
            double dValue = (double)value;

            return (int)((dMax / dH) * dValue);
        }

        public double doubleValue2Percent(double value, double max)
        {
            double dH = (double)100;
            double dMax = max;
            double dValue = value;
            return (dH / dMax) * dValue;
        }

        public double percentValue2double(double value, double max)
        {
            double dH = (double)100;
            double dMax = max;
            double dValue = value;

            return ((dMax / dH) * dValue);
        }

        public double getPlayerPositionByTimestamp(double xPosition, double maxXposition)
        {
            

            return 0;

        }
    }

}
