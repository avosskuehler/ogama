using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ogama.Modules.Rta.RtaReplay
{
    public class ActionController
    {
        private Action action = null;
        private bool isLogging = false;
        
        private Stack<Action> actions = new Stack<Action>();
        
        private void add(Action action)
        {
            actions.Push(action);
        }

        public void revert()
        {
            if (!this.hasActions())
            {
                return;
            }
            Action action = actions.Pop();
            if (action == null)
            {
                return;
            }
            log("revert:" + action.ToString());
            if (action.move)
            {
                action.figure.move(action.startPositionX, 0, 0, 0);
            }
        }



        public bool hasActions()
        {
            return actions.Count > 0;
        }

        public Action createAction()
        {
            this.action = new Action();
            return this.action;
        }

        public void setAction(Action action)
        {
            this.action = action;
        }
        
        public Action getAction()
        {
            return this.action;
        }

        public void onActionPerformed()
        {
            if (this.action == null)
            {
                return;
            }
            log("perform action:"+action.ToString());
            this.add(this.action);
            this.action = null;
        }

        public void updateActionEndPositionX(int endPositionX)
        {
            if (this.action == null)
            {
                return;
            }
            this.action.endPositionX = endPositionX;
        }

        public void onMove()
        {
            if (this.action == null)
            {
                return;
            }
            this.action.move = true;
        }
        public void onDelete()
        {
            if (this.action == null)
            {
                return;
            }
            this.action.delete = true;
        }

        public void onResize()
        {
            if (this.action == null)
            {
                return;
            }
            this.action.resize = true;
        }

        private void log(string s)
        {
            if (!isLogging)
            {
                return;
            }
            Console.WriteLine("ActionController.log:" + s);
        }
    }
}
