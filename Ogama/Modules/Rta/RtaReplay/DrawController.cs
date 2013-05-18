using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Ogama.Modules.Rta.Model;

namespace Ogama.Modules.Rta.RtaReplay
{
    public class DrawController
    {
        public bool mouseDown;
        private int mouseDownPositionX = 0;
        private int mouseUpPositionX = 0;
        private int figureTouchPointX = 0;

        private static IFigure NO_FIGURE = new Marker();
        private IFigure selectedFigure = NO_FIGURE;
        public Graphics g;
        public Color BackgroundColor = Color.DarkGray;
        public Marker marker1;
        public Marker marker2;
        public Marker progressMarker;
        private Segment newSegment = null;
        private IComparer<IFigure> figureComparator = null;
        IDrawControllerListener listener;
        private System.Object DRAW_LOCK = new System.Object();
        private List<IFigure> figures = new List<IFigure>();
        public int graphicsWidthMaxValue = 0;
        public double progressInPercent = 0;
        private Tools tools = new Tools();

        private RtaCategoryModel model;
        private RtaCategory rtaCategory;
        private ActionController actionController = new ActionController();

        public void setRtaCategory(RtaCategory rtaCategory)
        {
            this.rtaCategory = rtaCategory;
        }

        public void setActionController(ActionController actionController)
        {
            this.actionController = actionController;
        }

        public void setRtaCategoryModel(RtaCategoryModel model)
        {
            this.model = model;
        }

        public bool isValidProgressMarkerPosition(int x)
        {
            return isValidMarkerMove(this.progressMarker, x);
        }


        public void setProgressMarkerPositionInPercent(double percentValue, Graphics g)
        {
            int maxValue = this.progressMarker.maxXvalue;
            int markerValue = tools.percentValue2Int(percentValue, maxValue);
            this.progressMarker.positionX = markerValue;
            this.drawFigures(g);
        }



        public bool hasActions()
        {
            return this.actionController.hasActions();
        }

        public void onRevert()
        {
            this.actionController.revert();
        }

        public void addSegment(Segment segment)
        {
            if (segment == null)
            {
                return;
            }

            RtaEvent rtaEvent = new RtaEvent();
            rtaEvent.fkRtaCategory = this.rtaCategory;
            rtaEvent.Xstart = segment.getDimension().X;
            rtaEvent.Xend = segment.getDimension().Right;
            rtaEvent.startTimestamp = this.listener.getCurrentPlayerPosition();
            rtaEvent.endTimestamp = rtaEvent.startTimestamp;

            segment.rtaEvent = rtaEvent;

            this.model.Add(rtaEvent); 
        }

        public void onSegmentChanged(Segment changedSegment)
        {
            RtaEvent rtaEvent = changedSegment.rtaEvent;

            rtaEvent.Xstart = changedSegment.positionX;
            rtaEvent.Xend = changedSegment.positionX + changedSegment.width;

        }

        public void addRtaEvent(RtaEvent rtaEvent)
        {
            if (rtaEvent == null)
            {
                return;
            }
            Segment segment = new Segment();
            segment.positionX = (int)rtaEvent.Xstart;
            segment.width = (int)rtaEvent.Xend;
            segment.rtaEvent = rtaEvent;
            
            addToFigures(segment);
            
        }

       



        public void add(IFigure figure)
        {
            addToFigures(figure);

            if (figure is Segment)
            {
                Segment segment = (Segment)figure;

                this.addSegment(segment);
            }

        }

        private void addToFigures(IFigure figure)
        {
            this.figures.Add(figure);
            figures.Sort(this.getFigureComparator());
        }

        public void remove(IFigure figure)
        {
            this.figures.Remove(figure);
            if (figure is Segment)
            {
                Segment segment = (Segment)figure;
                this.model.RemoveEvent(segment.rtaEvent);
            }
        }


        public void drawFigures(Graphics g)
        {
            try
            {
                lock (this.DRAW_LOCK)
                {
                    g.Clear(this.BackgroundColor);

                    figures.ForEach(delegate(IFigure figure)
                    {
                        figure.draw(g);
                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
           
        }

    
        private IComparer<IFigure> getFigureComparator()
        {
            if (this.figureComparator == null)
            {
                this.figureComparator = new FigureComparator();
            }
            return this.figureComparator;
        }





     

        public void onMouseMove(int x, int y, Graphics g, int graphicsWidth)
        {
            if (!mouseDown)
            {
                return;
            }

            if(selectedFigure.Equals(NO_FIGURE))
            {
                if (!this.drawNewSegment(x, y, g))
                {
                    return;
                }
            }

            if (!isValidMove(selectedFigure, graphicsWidth, x, y, this.figureTouchPointX))
            {
                return;
            }


            selectedFigure.move(x, y, this.figureTouchPointX, this.mouseDownPositionX);

            moveMarker();
            Boolean segmentHasChanged = false;
            Boolean segmentHasMoved = false;
            
            if (selectedFigure.wasResized())
            {
                this.actionController.onResize();
                segmentHasChanged = true;
            }
            else
            {
                this.actionController.onMove();
                segmentHasMoved = true;
            }
            
            if (segmentHasChanged || segmentHasMoved)
            {
                if (selectedFigure is Segment)
                {
                    Segment changedSegment = (Segment)selectedFigure;
                    this.onSegmentChanged(changedSegment);
                }
            }

            drawFigures(g);

        }


      

        private void moveMarker()
        {
            if (! (selectedFigure is Marker))
            {
                return;
            }
            
            Marker marker = (Marker)selectedFigure;
            if (this.listener == null)
            {
                return;
            }
            
            double value = tools.intValue2Percent(marker.positionX, marker.maxXvalue);
            if (marker.isProgressMarker)
            {
                this.listener.onProgressTrackerPositionChanged(value);
            }
            else if (marker.leftMarker)
            {
                this.listener.onLeftMarkerPositionChanged(value);
                if (this.progressMarker.positionX < marker.positionX)
                {
                    this.progressMarker.positionX = marker.positionX;
                    this.listener.onProgressTrackerPositionChanged(value);
                }
            }
            else
            {
                this.listener.onRightMarkerPositionChanged(value);
                if (this.progressMarker.positionX > marker.positionX)
                {
                    this.progressMarker.positionX = marker.positionX;
                    this.listener.onProgressTrackerPositionChanged(value);
                }
            }
            
        }

       

        private bool drawNewSegment(int x, int y, Graphics g)
        {
            if (newSegment == null)
            {
                newSegment = new Segment();
                newSegment.positionX = x;
                this.add(newSegment);
                this.selectedFigure = newSegment;
                return true;
            }
            return false;
        }

        private bool isValidMove(IFigure figure, int graphicsWidth, int x, int y, int figureTouchPoint)
        {
           
            if (!isInBoundaries(figure, graphicsWidth, x, figureTouchPoint))
            {
                return false;
            }

            if (!isValidMarkerMove(figure, x))
            {
                return false;
            }

            return true;
        }

        private bool isValidMarkerMove(IFigure figure, int x)
        {
            if (figure.Equals(marker1))
            {
                if (x > marker2.positionX)
                {
                    return false;
                }
            }
            else if (figure.Equals(marker2))
            {
                if (x < marker1.positionX)
                {
                    return false;
                }
            }
            else if (figure.Equals(progressMarker))
            {
                if (x < marker1.positionX)
                {
                    return false;
                }
                else if (x > marker2.positionX)
                {
                    return false;
                }
            }

            return true;
        }

        private static bool isInBoundaries(IFigure figure, int graphicsWidth, int x, int figureTouchPoint)
        {
            
            if (x < 0)
            {
                return false;
            }
            if ((x + figure.getDimension().Width - figureTouchPoint) > graphicsWidth)
            {
                return false;
            }

            return true;
        }




        private IFigure selectFigure(int x, int y)
        {
            IFigure selectedFigure = NO_FIGURE;
            for (int i = figures.Count()-1; i >= 0; i--)
            {
                IFigure figure = figures.ElementAt(i);
                Rectangle dim = figure.getDimension();
                Rectangle test = new Rectangle(new Point(x, y), new Size(1, 1));
                if (dim.IntersectsWith(test))
                {
                    selectedFigure = figure;
                    selectedFigure.select(true);
                }
                else
                {
                    figure.select(false);
                }
            }
            return selectedFigure;
        }

        
        public void onMouseDown(int x)
        {
            this.mouseDown = true;
            this.mouseDownPositionX = x;
            this.selectedFigure = selectFigure(x,0);
            if(selectedFigure.Equals(NO_FIGURE))
            {
                return;
            }
            int figureX = this.selectedFigure.getDimension().X;
            this.figureTouchPointX = mouseDownPositionX - figureX;
            if(selectedFigure is Marker)
            {
                return;
            }
            createNewAction(x- figureTouchPointX);
        }

        private void createNewAction(int x)
        {
            Action action = this.actionController.createAction();
            action.startPositionX = x;
            action.figure = this.selectedFigure;
        }

        private void fireActionExecuted(int positionX)
        {
            if (this.actionController.getAction() == null)
            {
                return;
            }
            this.actionController.getAction().endPositionX = positionX;
            
        }

        public void onMouseUp(int x)
        {
            this.mouseDown = false;
            this.mouseUpPositionX = x;
            if (this.selectedFigure == null)
            {
                return;
            }
            this.selectedFigure.onMouseUp(x);

            
            if (this.selectedFigure.isDeleted())
            {
                this.remove(this.selectedFigure);
                this.actionController.onDelete();
            }
            this.actionController.updateActionEndPositionX(x- figureTouchPointX);
            this.actionController.onActionPerformed();


            this.selectedFigure = null;
            this.newSegment = null;
        }

        public void register(IDrawControllerListener li)
        {
            this.listener = li;
        }   

        class FigureComparator : IComparer<IFigure>
        {
            public int Compare(IFigure x, IFigure y)
            {
                if (x.getZlevel() < y.getZlevel())
                {
                    return 1;
                }
                else if (x.getZlevel() > y.getZlevel())
                {
                    return -1;
                }
                return 0;
            }
        }

        private void log(string s)
        {
            Console.WriteLine("DrawController.log:" + s);
        }
    }
}
