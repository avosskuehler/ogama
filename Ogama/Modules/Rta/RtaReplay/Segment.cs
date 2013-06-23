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


        private int mouseDownX = -1;
        private int figureTouchPointWidthDiff = 0;
        private int initialWidth = 0;
        
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
                this.width = initialWidth + newX;
            }
            else if (resizeLeft)
            {
                int transitionX = newX + figureTouchPointX;
                this.positionX = currentPositionX + figureTouchPointX;
                this.width = initialWidth - transitionX;
            }
            else
            {
                this.positionX = currentPositionX - figureTouchPointX;
            }

        }






        int newWidthDiff = 0;

        public void move4(int currentPositionX, int currentPositionY, int figureTouchPointX, int mouseDownPositionX)
        {
            int newX = currentPositionX - figureTouchPointX;
            int translationX = currentPositionX - mouseDownPositionX;
            if (this.positionX + figureTouchPointX == mouseDownPositionX)
            {
                figureTouchPointWidthDiff = this.positionX + width - (mouseDownPositionX);
            }
            if (lastMouseDownPositionX != mouseDownPositionX)
            {
                initialWidth = this.width;
            }
            lastMouseDownPositionX = mouseDownPositionX;

            log("move(" + currentPositionX + "," + currentPositionY + "," + figureTouchPointX + "," + mouseDownPositionX + ")," + figureTouchPointWidthDiff);

            if (currentPositionX >= this.positionX + this.width - figureTouchPointWidthDiff)
            {
                //resize right
                int newWidth = initialWidth + translationX - figureTouchPointWidthDiff;
                this.width = newWidth;
                //log("width:" + width);
            }





            if (isResizeLeft(figureTouchPointX))
            {
               /* if (translationX < 0)
                {
                    translationX += figureTouchPointX;
                }
                else
                {
                    translationX -= figureTouchPointX;
                }
                this.positionX += translationX;
                this.width -= translationX;*/
            }
            else if (isResizeRight(figureTouchPointX) || isResizeRight(figureTouchPointX + newWidthDiff))
            {
               /* if (lastMouseDownPositionX != mouseDownPositionX)
                {
                    initialWidth = this.width;
                }
                lastMouseDownPositionX = mouseDownPositionX;
                int newWidth = initialWidth + translationX;
                newWidthDiff = newWidth - width;
                this.width = newWidth;
                */
            }
            else
            {
                
                //this.positionX = newX;
            }
            
        }

      /*  int lastCurrentPositionX = 0;
        public void move3(int currentPositionX, int currentPositionY, int figureTouchPointX, int mouseDownPositionX)
        {
         
            int translationX = currentPositionX - mouseDownPositionX;
            if (translationX == 0)
            {
                translationX = mouseDownPositionX;
            }

            if (isResizeLeft(figureTouchPointX))
            {
                resizeLeft = true;
                if (translationX < 0)
                {
                    translationX += mouseDownPositionX;
                }
                this.positionX += translationX;
                this.width -= translationX;
            }
            else if (isResizeRight(figureTouchPointX))
            {
                resizeRight = true;
                if (translationX > 0)
                {
                    translationX -= (width - figureTouchPointX);
                }
                this.width += translationX;
            }
            else
            {
                this.positionX += translationX;
            }

            lastCurrentPositionX = currentPositionX;

        }
        */

        private bool isResizeRight(int figureTouchPointX)
        {
            int a = figureTouchPointX;
            int b = width - segmentActiveCornerWidth;

            //log("isRisizeRight ("+a+">="+b+")");
            return a >= b;

        }

        private bool isResizeLeft(int figureTouchPointX)
        {
            return figureTouchPointX < segmentActiveCornerWidth;
        }

        /// <summary>
        /// moving or resizing the segment
        /// </summary>
        /// <param name="currentPositionX">the current mouse x-position</param>
        /// <param name="currentPositionY">the x-position, at which the mouse has hidden the segment</param>
        /// <param name="figureTouchPointX">the x-position, at which the mouse has hidden the segment</param>
        /// <param name="mouseDownPositionX">the initial x-position the mouse has been, when a mouse button was pressed</param>
        public void move_old(int currentPositionX, int currentPositionY, int figureTouchPointX, int mouseDownPositionX)
        {
            this.positionY = currentPositionY;
           
            
            if (this.positionY < deltaMinY)
            {
                return;
            }
            else
            {
                this.positionY = 0;
            }
            int deltaX = mouseDownPositionX - currentPositionX;
            int np = currentPositionX - figureTouchPointX;
            int diffX = (np - mouseDownPositionX);
            
            
            int translationX = (mouseDownPositionX - currentPositionX);
           
            bool increase = false;

            
            int translationXdiff = (lastTranslationX - translationX);
            int translationXdiffAbs = Math.Abs(translationXdiff);


            if (figureTouchPointX < segmentActiveCornerWidth)
            {
                resizeLeft = true;
                if (translationXdiff < 0)
                {
                    increase = true;    
                }
                this.lastTranslationX = translationX;
            }
            else if (figureTouchPointX > (width - segmentActiveCornerWidth) || resizeRight)
            {
                resizeRight = true;
                if (translationXdiff > 0) 
                {
                    increase = true;
                }
                this.lastTranslationX = translationX;
            }
            else
            {
                resizeLeft = false;
                resizeRight = false;
                lastTranslationX = 0;
            }

            int newWidth = Math.Abs(translationX);
            int add2 = Math.Abs(this.positionX - currentPositionX);
            int add = translationXdiffAbs;
            
            if (resizeLeft)
            {
                //TEST
                add = add2;

                //this.positionX = (currentPositionX - figureTouchPointX);
                if (increase)
                {
                    this.width += add;
                    this.positionX -= add;
                }
                else
                {
                    int withUpdate = this.width - add;
                    if (withUpdate > minWidth)
                    {
                        this.width = withUpdate;
                        this.positionX += add;
                    }
                }
            }
            else if (resizeRight)
            {
                if (increase)
                {
                    this.width += add;
                }
                else
                {
                    int withUpdate = this.width - add;
                    if (withUpdate > minWidth)
                    {
                        this.width = withUpdate;
                    }
                }
            }
            else
            {
                this.positionX = (currentPositionX - figureTouchPointX);   
            }

            this.rtaEvent.Xstart = this.positionX;
            this.rtaEvent.Xend = this.getDimension().Right;
            
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
