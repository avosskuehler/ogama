using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Ogama.Modules.Rta.RtaReplay
{
    public class Marker : IFigure
    {
        public int positionX;
        public int height = 50;
        public int width = 5;
        public Brush brush = Brushes.Black;
        public bool leftMarker = false;
        public int maxXvalue = 0;
        public bool isProgressMarker = false;
        private int zLevel = 0;

        public void onMouseUp(int x)
        {

        }

        public void select(bool selected)
        {

        }

        public bool wasResized()
        {
            return false;
        }

        public bool isDeleted()
        {
            return false;
        }

        public bool isValidMove(IFigure figure, int x, int y)
        {
            if (figure is Marker)
            {
                Marker aMarker = (Marker)figure;
                if (this.positionX < aMarker.positionX)
                {
                    if (x <= this.positionX)
                    {
                        return false;
                    }
                }
                else
                {
                    if (x >= this.positionX)
                    {
                        //return false;
                    }
                }
                
            }
            return true;
        }

        public bool isValidMove(List<IFigure> figures, int x, int y)
        {
            for (int i = 0; i < figures.Count(); i++)
            {
                IFigure figure = figures.ElementAt(i);
                if (!isValidMove(figure, x, y))
                {
                    return false;
                }
            }
            return true;
        }

        public void move(int currentPositionX, int currentPositionY, int figureTouchPointX, int mouseDownPositionX)
        {
            this.positionX = (currentPositionX - figureTouchPointX);
        }

        public void draw(Graphics g)
        {
            Brush scopeBrush = Brushes.WhiteSmoke;
            
            if (leftMarker)
            {
                g.FillRectangle(scopeBrush, 0, 0, positionX, height);
            }
            else if (isProgressMarker)
            {

            }
            else
            {
                int x1 = positionX + width;
                int y1 = 0;
                int x2 = maxXvalue;
                int y2 = height;
                g.FillRectangle(scopeBrush, x1, y1, x2, y2);
            }

            int w = width;
            g.FillRectangle(brush, positionX, 0, w, height);
            int x = positionX + (w/2)-1;
            int y = 0;
            int d = 5;
            g.FillPolygon(brush, new Point[] { 
                new Point(x - d, 0)
                ,new Point(x ,y + 10)
                ,new Point(x + d*2, y)
            });
         
        }

        public int getZlevel()
        {
            return zLevel;
        }

        public void setZlevel(int level)
        {
            this.zLevel = level;
        }

        public Rectangle getDimension()
        {
            Rectangle rect = new Rectangle();
            rect.X = this.positionX;
            rect.Y = 0;
            rect.Width = this.width;
            rect.Height = this.height;
            return rect;
        }

        public override string ToString()
        {
            string s = "Marker:";
            if (isProgressMarker)
            {
                s += " ProgressMarker.x:" + positionX;
            }
            return s;
        }
    }
}
