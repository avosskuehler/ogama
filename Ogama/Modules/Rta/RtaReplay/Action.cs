using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ogama.Modules.Rta.RtaReplay
{
    public class Action
    {
        public int startPositionX;
        public int startPositionY;
        public int endPositionX;
        public int endPositionY;
        public bool move;
        public bool resize;
        public bool delete;
        public bool create;
        public IFigure figure;

        
        public override string ToString()
        {
            string s = "Action:"
                +" \n startPositionX:"+startPositionX
                + "\n startPositionY:" + startPositionY
                + "\n endPositionX:" + endPositionX
                + "\n endPositionY:" + endPositionY
                + "\n move:" + move
                + "\n resize:" + resize
                + "\n delete:" + delete
                + "\n create:" + create
            ;
            return s;
        }
    }
}
