using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ogama.Modules.Rta.RtaReplay
{
    public class MessageQueue
    {
        private static MessageQueue instance;

        private static Object LOCK = new Object();

        public static MessageQueue getInstance()
        {
            lock (LOCK)
            {
                if (instance == null)
                {
                    instance = new MessageQueue();
                }
                return instance;
            }
        }


        public void add()
        {
            
        }

    }
}
