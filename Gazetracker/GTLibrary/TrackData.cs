using System.Drawing;

namespace GTLibrary
{
  using GTLibrary.Calibration;
  using GTLibrary.Detection.Glint;
  using GTLibrary.Detection.Pupil;
  using GTLibrary.EyeMovement;
  using GTLibrary.Utils;

  public class TrackData
    {
        #region Variables 

        private CalibrationData calibrationDataLeft;
        private CalibrationData calibrationDataRight;
        private bool eyeDetected;

        private Classifier.EyeMovementStateEnum eyeMovement;
        
        private long timeStamp;
        private bool eyesDetected;
        private Rectangle cameraROI = new Rectangle(0,0,0,0);
        private Rectangle eyesROI;

        private long frameNumber;
        private GTGazeData gazeDataRaw;
        private GTGazeData gazeDataSmoothed;
        private GlintData glintDataLeft;
        private GlintData glintDataRight;
        private bool glintsLeftDetected;
        private bool glintsRightDetected;
        private Rectangle leftROI;
        private bool processingOk;
        private PupilData pupilDataLeft;
        private PupilData pupilDataRight;
        private bool pupilLeftDetected;
        private bool pupilRightDetected;
        private Rectangle rightROI;


        private int unfilteredBlobCountLeft;
        private int unfilteredBlobCountRight;

        private double unfilteredTotalBlobAreaLeft;
        private double unfilteredTotalBloblAreaRight;

        #endregion

        #region Constructor

        public TrackData()
        {
            eyesROI = new Rectangle();
            leftROI = new Rectangle();
            rightROI = new Rectangle();

            pupilDataLeft = new PupilData();
            pupilDataRight = new PupilData();

            glintDataLeft = new GlintData();
            glintDataRight = new GlintData();

            calibrationDataLeft = new CalibrationData();
            calibrationDataRight = new CalibrationData();

            gazeDataRaw = new GTGazeData();
            gazeDataSmoothed = new GTGazeData();

            //eyeMovement = new GazeTrackingLibrary.EyeMovement.Classifier();

            eyesDetected = false;
            eyeDetected = false;
            pupilLeftDetected = false;
            pupilRightDetected = false;
            glintsLeftDetected = false;
            glintsRightDetected = false;
        }

        #endregion

        #region Get/Set

        public long TimeStamp
        {
            get { return timeStamp; }
            set { timeStamp = value; }
        }

        public long FrameNumber
        {
            get { return frameNumber; }
            set { frameNumber = value; }
        }

        public Rectangle CameraROI
        {
            get { return cameraROI; }
            set { cameraROI = value; }
        }

        public Rectangle EyesROI
        {
            get { return eyesROI; }
            set { eyesROI = value; }
        }

        public Rectangle LeftROI
        {
            get { return leftROI; }
            set { leftROI = value; }
        }

        public Rectangle RightROI
        {
            get { return rightROI; }
            set { rightROI = value; }
        }

        public PupilData PupilDataLeft
        {
            get { return pupilDataLeft; }
            set
            {
                pupilDataLeft = value;

                if (pupilDataLeft.Blob != null && pupilDataLeft.Blob.Area != 0)
                    pupilLeftDetected = true;
                else
                    pupilLeftDetected = false;
            }
        }

        public PupilData PupilDataRight
        {
            get { return pupilDataRight; }
            set
            {
                pupilDataRight = value;

                if (pupilDataRight.Blob != null && pupilDataRight.Blob.Area != 0)
                    pupilRightDetected = true;
                else
                    pupilRightDetected = false;
            }
        }

        public GlintData GlintDataLeft
        {
            get { return glintDataLeft; }
            set
            {
                glintDataLeft = value;

                if (glintDataLeft != null && glintDataLeft.Glints.Count != 0)
                    glintsLeftDetected = true;
                else
                    glintsLeftDetected = false;
            }
        }

        public GlintData GlintDataRight
        {
            get { return glintDataRight; }
            set
            {
                glintDataRight = value;

                if (glintDataRight != null && glintDataRight.Glints.Count != 0)
                    glintsRightDetected = true;
                else
                    glintsRightDetected = false;
            }
        }

        public CalibrationData CalibrationDataLeft
        {
            get { return calibrationDataLeft; }
            set { calibrationDataLeft = value; }
        }

        public CalibrationData CalibrationDataRight
        {
            get { return calibrationDataRight; }
            set { calibrationDataRight = value; }
        }

        public Classifier.EyeMovementStateEnum EyeMovement
        {
            get { return eyeMovement; }
            set { eyeMovement = value; }
        }

        public bool EyesDetected
        {
            get { return eyesDetected; }
            set { eyesDetected = value; }
        }

        public bool EyeDetected
        {
            get { return eyeDetected; }
            set { eyeDetected = value; }
        }

        public bool PupilLeftDetected
        {
            get { return pupilLeftDetected; }
        }

        public bool PupilRightDetected
        {
            get { return pupilRightDetected; }
            set { pupilRightDetected = value; }
        }

        public bool GlintsLeftDetected
        {
            get { return glintsLeftDetected; }
            set { glintsLeftDetected = value; }
        }

        public bool GlintsRightDetected
        {
            get { return glintsRightDetected; }
            set { glintsRightDetected = value; }
        }

        public int UnfilteredBlobCountRight
        {
            get { return unfilteredBlobCountRight; }
            set { unfilteredBlobCountRight = value; }
        }

        public int UnfilteredBlobCountLeft
        {
            get { return unfilteredBlobCountLeft; }
            set { unfilteredBlobCountLeft = value; }
        }

        public double UnfilteredTotalBlobAreaLeft
        {
            get { return unfilteredTotalBlobAreaLeft; }
            set { unfilteredTotalBlobAreaLeft = value; }
        }

        public double UnfilteredTotalBlobAreaRight
        {
            get { return unfilteredTotalBlobAreaLeft; }
            set { unfilteredTotalBlobAreaLeft = value; }
        }

        public GTGazeData GazeDataRaw
        {
            get { return gazeDataRaw; }
            set { gazeDataRaw = value; }
        }

        public GTGazeData GazeDataSmoothed
        {
            get { return gazeDataSmoothed; }
            set { gazeDataSmoothed = value; }
        }

        public bool ProcessingOk
        {
            get { return processingOk; }
            set { processingOk = value; }
        }

        #endregion

        #region Public methods

        public TrackData Copy()
        {
            // Use shallow copy instead? 

            var t = new TrackData
                        {
                            calibrationDataLeft = calibrationDataLeft,
                            calibrationDataRight = calibrationDataRight,
                            eyeDetected = eyeDetected,
                            eyeMovement = eyeMovement,
                            eyesDetected = eyesDetected,
                            cameraROI = cameraROI,
                            eyesROI = eyesROI,
                            frameNumber = frameNumber,
                            glintDataLeft = glintDataLeft,
                            glintDataRight = glintDataRight,
                            glintsLeftDetected = glintsLeftDetected,
                            glintsRightDetected = glintsRightDetected,
                            leftROI = leftROI,
                            pupilDataLeft = pupilDataLeft,
                            pupilDataRight = pupilDataRight,
                            pupilLeftDetected = pupilLeftDetected,
                            pupilRightDetected = pupilRightDetected,
                            rightROI = rightROI,
                            timeStamp = timeStamp,
                            unfilteredBlobCountLeft = unfilteredBlobCountLeft,
                            unfilteredBlobCountRight = unfilteredBlobCountRight,
                            unfilteredTotalBlobAreaLeft = unfilteredTotalBlobAreaLeft,
                            unfilteredTotalBloblAreaRight = unfilteredTotalBloblAreaRight,
                            gazeDataRaw = gazeDataRaw,
                            gazeDataSmoothed = gazeDataSmoothed,
                            processingOk = processingOk
                        };

            return t;
        }

        #endregion
    }
}