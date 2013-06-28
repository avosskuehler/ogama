using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ogama.Modules.Rta.RtaReplay;

namespace OgamaTestProject.Modules.Rta.RtaReplay
{
    [TestClass]
    public class SegmentTest
    {
        [TestMethod]
        public void TestShrinkLeft()
        {
            Segment cut = new Segment();
            cut.rtaEvent = new OgamaDao.Model.Rta.RtaEvent();
            
            int currentPositionX = 5;
            int figureTouchPointX = 0;
            int mouseDownPositionX = 0;
            int initialWidth = 50;

            cut.positionX = 0;
            cut.width = initialWidth;
            cut.onMouseDown(mouseDownPositionX);

            cut.move(currentPositionX, 0, figureTouchPointX, mouseDownPositionX);

            Assert.AreEqual(currentPositionX, cut.positionX);
            Assert.AreEqual(initialWidth - currentPositionX, cut.width);
        }

        [TestMethod]
        public void TestShrinkRight()
        {
            Segment cut = new Segment();
            cut.rtaEvent = new OgamaDao.Model.Rta.RtaEvent();

            int initialWidth = 50;
            int mouseDownPositionX = 48;
            int currentPositionX = 47;
            int figureTouchPointX = 48;
            
            cut.positionX = 0;
            cut.width = initialWidth;
            cut.onMouseDown(mouseDownPositionX);

            cut.move(currentPositionX, 0, figureTouchPointX, mouseDownPositionX);

            Assert.AreEqual(0, cut.positionX);
            Assert.AreEqual(49, cut.width);
        }

        [TestMethod]
        public void TestIncreaseRight()
        {
            Segment cut = new Segment();
            cut.rtaEvent = new OgamaDao.Model.Rta.RtaEvent();

            int initialWidth = 50;
            int mouseDownPositionX = 48;
            int currentPositionX = 51;
            int figureTouchPointX = 48;

            cut.positionX = 0;
            cut.width = initialWidth;
            cut.onMouseDown(mouseDownPositionX);

            cut.move(currentPositionX, 0, figureTouchPointX, mouseDownPositionX);

            Assert.AreEqual(0, cut.positionX);
            Assert.AreEqual(53, cut.width);


        }

        [TestMethod]
        public void TestIncreaseLeft()
        {
            Segment cut = new Segment();
            cut.rtaEvent = new OgamaDao.Model.Rta.RtaEvent();

            int initialWidth = 50;
            int mouseDownPositionX = 12;
            int currentPositionX = 5;
            int figureTouchPointX = 2;

            cut.positionX = 10;
            cut.width = initialWidth;
            cut.onMouseDown(mouseDownPositionX);

            cut.move(currentPositionX, 0, figureTouchPointX, mouseDownPositionX);

            Assert.AreEqual(7, cut.positionX);
            Assert.AreEqual(55, cut.width);
        }

        [TestMethod]
        public void TestMoveFromLeftToRight()
        {
            Segment cut = new Segment();
            cut.rtaEvent = new OgamaDao.Model.Rta.RtaEvent();

            int initialWidth = 50;
            int mouseDownPositionX = 10;
            int currentPositionX = 15;
            int figureTouchPointX = 10;
            

            cut.positionX = 0;
            cut.width = initialWidth;
            cut.onMouseDown(mouseDownPositionX);

            cut.move(currentPositionX, 0, figureTouchPointX, mouseDownPositionX);

            Assert.AreEqual(initialWidth, cut.width);
            Assert.AreEqual(currentPositionX - figureTouchPointX, cut.positionX);
            
        }

        [TestMethod]
        public void TestMoveFromRightToLeft()
        {
            Segment cut = new Segment();
            cut.rtaEvent = new OgamaDao.Model.Rta.RtaEvent();

            int initialWidth = 50;
            int mouseDownPositionX = 10;
            int currentPositionX = 5;
            int figureTouchPointX = 10;


            cut.positionX = 0;
            cut.width = initialWidth;
            cut.onMouseDown(mouseDownPositionX);

            cut.move(currentPositionX, 0, figureTouchPointX, mouseDownPositionX);

            Assert.AreEqual(initialWidth, cut.width);
            Assert.AreEqual(currentPositionX - figureTouchPointX, cut.positionX);

        }

        [TestMethod]
        public void TestMoveRight()
        {
            Segment cut = new Segment();
            cut.rtaEvent = new OgamaDao.Model.Rta.RtaEvent();
            cut.positionX = 0;
            cut.width = 50;
            

            int X = 20;
            cut.onMouseDown(X);

            cut.move(X, 0, 20, X);
            Assert.AreEqual(0, cut.positionX);

            cut.move(X+5, 0, 20, X);
            Assert.AreEqual(5, cut.positionX);

            cut.move(X, 0, 20, X);
            Assert.AreEqual(0, cut.positionX);



        }

        [TestMethod]
        public void TestIncreaseDecreaseRight()
        {
            Segment cut = new Segment();
            cut.rtaEvent = new OgamaDao.Model.Rta.RtaEvent();
            cut.positionX = 0;
            cut.width = 50;

            cut.onMouseDown(48);
            cut.move(48, 0, 48, 48);
            Assert.AreEqual(0, cut.positionX);
            Assert.AreEqual(50, cut.width);

            cut.move(49, 0, 48, 48);
            Assert.AreEqual(0, cut.positionX);
            Assert.AreEqual(51, cut.width);

            cut.move(50, 0, 48, 48);
            Assert.AreEqual(0, cut.positionX);
            Assert.AreEqual(52, cut.width);

            cut.move(51, 0, 48, 48);
            Assert.AreEqual(0, cut.positionX);
            Assert.AreEqual(53, cut.width);

            cut.move(49, 0, 48, 48);
            Assert.AreEqual(0, cut.positionX);
            Assert.AreEqual(51, cut.width);

            cut.move(48, 0, 48, 48);
            Assert.AreEqual(0, cut.positionX);
            Assert.AreEqual(50, cut.width);

            cut.move(60, 0, 48, 48);
            Assert.AreEqual(0, cut.positionX);
            Assert.AreEqual(62, cut.width);

            cut.move(50, 0, 48, 48);
            Assert.AreEqual(0, cut.positionX);
            Assert.AreEqual(52, cut.width);

            cut.move(70, 0, 48, 48);
            Assert.AreEqual(0, cut.positionX);
            Assert.AreEqual(72, cut.width);
        }


    }
}
