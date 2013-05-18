using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Ogama.Modules.Rta.Model;

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
        private int lastMouseDownPositionX = 0;
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

        public void onMouseUp(int x)
        {
            this.resizeRight = false;
        }

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

        public void move(int currentPositionX, int currentPositionY, int figureTouchPointX, int mouseDownPositionX)
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
