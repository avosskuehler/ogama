using System;
using System.IO;

using Emgu.CV;
using Emgu.CV.Structure;

using GTCommons.Enum;
using GTSettings;

using System.ComponentModel;

namespace GTLibrary.Calibration
{
  using GTCommons;

    using GTLibrary.Detection.Glint;
    using GTLibrary.Utils;

  // Main Calibration Class
    public class Calibration
    {
        #region Variables

        private readonly long id;
        private const string baseFolder = "Calibration";
        private CalibMethod calibMethod;
        private string dataPath;
        //private bool isSavingImages = true;
        //private string uniqueFolder = "";

        #endregion

        #region Constructor

        public Calibration()
        {
            id = GetUniqueID();

            if (Settings.Instance.Processing.TrackingGlints)
                calibMethod = new CalibPolynomial();
            else
                calibMethod = new CalibPupil();
        }

        #endregion

        #region Get/Set

        public CalibMethod CalibMethod
        {
            get { return calibMethod; }
            set { calibMethod = value; }
        }
        public long ID
        {
            get { return id; }
        }

        public bool IsCalibrating { get; set; }

        public bool IsCalibrated
        {
            get { return CalibMethod.IsCalibrated; }
            set { CalibMethod.IsCalibrated = value; }
        }

        public GTPoint PupilCenterLeft
        {
            get { return CalibMethod.PupilCenterLeft; }
            set { CalibMethod.PupilCenterLeft = value; }
        }

        public GTPoint PupilCenterRight
        {
            get { return CalibMethod.PupilCenterRight; }
            set { CalibMethod.PupilCenterRight = value; }
        }

        public GlintConfiguration GlintConfigLeft
        {
            get { return CalibMethod.GlintConfigLeft; }
            set { CalibMethod.GlintConfigLeft = value; }
        }

        public GlintConfiguration GlintConfigRight
        {
            get { return CalibMethod.GlintConfigRight; }
            set { CalibMethod.GlintConfigRight = value; }
        }

        public int InstanceTargetNumber
        {
            get { return CalibMethod.InstanceTargetNumber; }
            set { CalibMethod.InstanceTargetNumber = value; }
        }


        public string DataFolder
        {
            get { return dataPath; }
        }

        #endregion

        #region Public methods

        public bool Calibrate()
        {
            // Make folder 
            CreateOutputfolder();

            return calibMethod.Calibrate();
        }

        public void ExportToFile()
        {
            // Run the export to file through a backgroundworker so that the UI remains responsive
            BackgroundWorker bgExportCalibration = new BackgroundWorker();
            bgExportCalibration.DoWork += bgExportCalibration_DoWork;
            bgExportCalibration.RunWorkerAsync();
        }

        public GTPoint GetGazeCoordinates(TrackData trackData, EyeEnum eye)
        {
            return calibMethod.GetGazeCoordinates(trackData, eye);
        }

        public void SaveROIImage(long counter, Image<Gray, byte> inputEye, EyeEnum eyeEnum)
        {
            try
            {
                if (eyeEnum == EyeEnum.Left)
                    inputEye.Bitmap.Save(dataPath + "\\" + InstanceTargetNumber + "-" + counter + "-L.png");
                else
                    inputEye.Bitmap.Save(dataPath + "\\" + InstanceTargetNumber + "-" + counter + "-R.png");
            }
            catch (Exception)
            {
            }
        }

        #endregion

        #region Private methods

        private static long GetUniqueID()
        {
            var rand = new Random();
            var dt = DateTime.Now;

            // Eg. 20100115122354 + random value 1000 to 9999 
            // eg. maximum 10,000 unique calibrations per every second of every minute, every hour, day, month
            // ought be unqiue enough

            var today = string.Format("{0:yyyyMMddHHmmss}", dt);
            string strID = today + rand.Next(1000, 9999).ToString();
            var id = long.Parse(strID);

            //CheckIfIDExists(id); not sure we need it

            return id;
        }

        private void CreateOutputfolder()
        {
            // var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            var basePath = GTPath.GetLocalApplicationDataPath();

            // Check if default "Calibration" dir exists, if not create it
            if (!Directory.Exists(basePath + Path.DirectorySeparatorChar + baseFolder))
            {
                Directory.CreateDirectory(basePath + Path.DirectorySeparatorChar + baseFolder);
            }

            this.dataPath = basePath + Path.DirectorySeparatorChar + baseFolder + Path.DirectorySeparatorChar + this.id;

            if (!Directory.Exists(this.dataPath))
            {
                Directory.CreateDirectory(this.dataPath);
            }
        }

        private void bgExportCalibration_DoWork(object sender, DoWorkEventArgs e)
        {
            var calibExport = new CalibrationExport(this.id, this.dataPath);
            calibExport.GenerateXML(CalibMethod);
        }

        #endregion

    }
}