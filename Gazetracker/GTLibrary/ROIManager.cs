using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GazeTrackingLibrary.Utils;
using System.Drawing;

namespace GazeTrackingLibrary
{
    public class ROIManager
    {
        private static ROIManager instance;

        private ROIManager()
        {
          
        }



        public void SetROI(ROIType type, Rectangle rect)
        {
            Rectangle roi;

            switch (type)
            {
                case ROIType.Head:
                    //estimate eyes roi
                    break;
                case ROIType.Eyes:
                    //use roi
                    break;
            }
        }

        public void SetROI(ROIType type, Rectangle left, Rectangle right)
        {
            Rectangle roi;

            switch (type)
            {
                case ROIType.Eye:
                   // use rect
                    break;
                case ROIType.Pupil:
                    //build roi
                    break;
            }

            Camera.CameraControl.Instance.ROI = rect;
        }


        private Rectangle EstimateROIHead(Rectangle rectHead)
        {
            //if (CameraControl.Instance.IsROISet)
            //    return new Rectangle(0, 0, CameraControl.Instance.ROI.Width, CameraControl.Instance.ROI.Height);

            Rectangle estEyeROI = new Rectangle();
            estEyeROI.Y = Convert.ToInt32(face.Height * 2.8 / 11));
            estEyeROI.X = rectHead.X;
            estEyeROI.Size =  new Size(face.Width, (face.Height * 2 / 9));
            estEyeROI.Size = new Size(rectHead.Width, Camera.CameraControl.Instance.Capture.Height/6);

            return estEyeROI;
        }

        private Rectangle EstimateROIEyes(Rectangle left, Rectangle right)
        {
        }


        public Rectangle LeftEyeROI
        {
        }

        public Rectangle RightEyeROI
        {
        }


        public ROIManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ROIManager();
                }

                return instance;
            }

        }

    }
}
