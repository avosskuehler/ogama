using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Ogama.Modules.Rta.RtaReplay
{
    public interface IFigure
    {
        int getZlevel();
        
        Rectangle getDimension();
        
        void move(int currentPositionX, int currentPositionY, int figureTouchPointX,int mouseDownPositionX);
        
        bool wasResized();

        void draw(Graphics g);

        bool isValidMove(IFigure figure, int x, int y);

        bool isValidMove(List<IFigure> figures, int x, int y);

        void onMouseUp(int x);

        void select(bool selected);

        bool isDeleted();
    }
}
