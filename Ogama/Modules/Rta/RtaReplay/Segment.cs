using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using OgamaDao.Model.Rta;

namespace Ogama.Modules.Rta.RtaReplay
{
    public class Segment : IFigure
    {
        public int positionX = 0;
        private int positionY = 0;
        public int width = 50;
        public int height = 50;
        
        private int segmentActiveCornerWidth = 5;
        private int minWidth = 20;
        private int lastTranslationX = 0;
        private int lastMouseDownPositionX = -1;
        private bool resizeLeft = false;
        private bool resizeRight = false;
        private int deltaMinY = -10;
        private int mouseDownX = -1;
        private int figureTouchPointWidthDiff = 0;
        private int initialWidth = 0;
        
        private Brush DEFAULT_BACKGROUND_BRUSH = Brushes.LightGray;
        public Brush backgroundBrush = Brushes.LightGray;
        public RtaEvent rtaEvent{get;set;}

        public int getZlevel()
        {
            return 1;
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


        

        public void select(bool selected)
        {
            if (selected)
            {
                this.backgroundBrush = Brushes.Red;
            }
            else
            {
                this.backgroundBrush = DEFAULT_BACKGROUND_BRUSH;
            }
        }

        
        /// <summary>
        /// none
        /// </summary>
        /// <returns></returns>
        public bool isDeleted()
        {
            if (this.positionY < deltaMinY)
            {
                return true;
            }
            return false;
        }

        public bool wasResized()
        {
            if (resizeLeft || resizeRight)
            {
                return true;
            }
            return false;
        }

        public void onMouseUp(int x)
        {
            this.resizeRight = false;
            this.resizeLeft = false;
        }


     
        public void onMouseDown(int x)
        {
            mouseDownX = x;
            initialWidth = this.width;
            
            if (x >= this.positionX + this.width - segmentActiveCornerWidth)
            {
                resizeRight = true;
            }
            else if (x <= this.positionX + segmentActiveCornerWidth)
            {
                resizeLeft = true;
            }
        }

    
        public void move(int currentPositionX, int currentPositionY, int figureTouchPointX, int mouseDownPositionX)
        {
            int newX = currentPositionX - mouseDownX;
            
            if (resizeRight)
            {
                int newWidth = initialWidth + newX;
                if (newWidth >= minWidth)
                {
                    this.width = newWidth;
                }
            }
            else if (resizeLeft)
            {
                
                int transitionX = newX + figureTouchPointX;
                int newWidth = initialWidth - transitionX;
                if (newWidth >= minWidth)
                {
                    this.width = newWidth;
                    this.positionX = currentPositionX + figureTouchPointX;
                }
            }
            else
            {
                this.positionX = currentPositionX - figureTouchPointX;
            }

            updateRtaEvent();
        }

        protected void updateRtaEvent()
        {
            this.rtaEvent.Xstart = this.positionX;
            this.rtaEvent.Xend = this.positionX + this.width;
            log("rtaEvent (" + rtaEvent.Xstart + "," + rtaEvent.Xend+")");
        }

        public void draw(Graphics g)
        {
            if (this.positionY < -20)
            {
                this.backgroundBrush = Brushes.Green;
            }
            else
            {
                this.backgroundBrush = DEFAULT_BACKGROUND_BRUSH;
            }
            g.FillRectangle(backgroundBrush, positionX, positionY, width, height);
            Brush cornerBrush = Brushes.Yellow;
            g.FillRectangle(cornerBrush, positionX, positionY, segmentActiveCornerWidth, height);
            int offsetX = positionX + width - segmentActiveCornerWidth;
            g.FillRectangle(cornerBrush, offsetX, positionY, segmentActiveCornerWidth, height);
            
        }



        public bool isValidMove(IFigure figure, int x, int y)
        {
            return true;
        }

        public bool isValidMove(List<IFigure> figures, int x, int y)
        {
            return true;
        }

        private void log(string s)
        {
            Console.WriteLine("Segment.log:" + s);
        }
    }
}
