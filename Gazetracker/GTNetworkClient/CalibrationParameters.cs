using System.Windows.Media;
using GTCommons;

namespace GTNetworkClient
{
    public class CalibrationParameters
    {
        public const string strNumberOfPoints = "NumberOfPoints";
        public const string strSpeed = "Speed";
        public const string strBackgroundColor = "BackgroundColor";
        public const string strPointColor = "PointColor";
        public const string strUseInfantGraphics = "UseInfantGraphics";
        public const string strRandomizePointOrder = "RandomizePointOrder";
        public const string strAutoAcceptPoints = "AutoAcceptPoints";
        public const string strWaitForValidData = "WaitForValidData";
        private bool autoAccept = true;

        private SolidColorBrush backgroundColor = new SolidColorBrush(Colors.DarkGray);
        private int numberOfPoints = 9;
        private SolidColorBrush pointColor = new SolidColorBrush(Colors.White);
        private bool randomizeOrder = true;
        private double speed = 1;
        private bool useInfantGraphics;
        private bool waitForValidData;

        public string ParametersAsString
        {
            get
            {
                string paramStr =
                    strNumberOfPoints + "=" + numberOfPoints + "," +
                    strSpeed + "=" + speed + "," +
                    strBackgroundColor + "=" + backgroundColor.Color.R + "-" + backgroundColor.Color.G + "-" +
                    backgroundColor.Color.B + "," +
                    strPointColor + "=" + pointColor.Color.R + "-" + pointColor.Color.G + "-" + pointColor.Color.B + "," +
                    strUseInfantGraphics + "=" + useInfantGraphics + "," +
                    strRandomizePointOrder + "=" + RandomizePointOrder + "," +
                    strAutoAcceptPoints + "=" + AutoAcceptPoints + "," +
                    strWaitForValidData + "=" + waitForValidData;

                return paramStr;
            }
        }

        public int NumberOfPoints
        {
            get { return numberOfPoints; }
            set { numberOfPoints = value; }
        }

        public double Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public SolidColorBrush BackgroundColor
        {
            get { return backgroundColor; }
            set { backgroundColor = value; }
        }

        public SolidColorBrush PointColor
        {
            get { return pointColor; }
            set { pointColor = value; }
        }

        public bool UseInfantGraphics
        {
            get { return useInfantGraphics; }
            set { useInfantGraphics = value; }
        }

        public bool WaitForValidData
        {
            get { return waitForValidData; }
            set { waitForValidData = value; }
        }

        public bool RandomizePointOrder
        {
            get { return randomizeOrder; }
            set { randomizeOrder = value; }
        }

        public bool AutoAcceptPoints
        {
            get { return autoAccept; }
            set { autoAccept = value; }
        }

        public void SetDefaultParameters()
        {
            NumberOfPoints = 9;
            RandomizePointOrder = true;
            AutoAcceptPoints = true;
            WaitForValidData = true;
        }

        public void ExtractParametersFromString(string parameterStr)
        {
            // Seperating commands
            char[] sepCalibrationParameters = {','};
            string[] calibrationParams = parameterStr.Split(sepCalibrationParameters, 20);

            // Seperating values/parameters
            char[] sepValues = {'='};

            var calParams = new CalibrationParameters();

            for (int i = 0; i < calibrationParams.Length; i++)
            {
                string[] cmdString = calibrationParams[i].Split(sepValues, 5);
                string subCmdStr = cmdString[0];
                string value = cmdString[1];

                switch (subCmdStr)
                {
                    case strNumberOfPoints:
                        NumberOfPoints = int.Parse(value);
                        break;

                    case strSpeed:
                        Speed = double.Parse(value);
                        break;

                    case strBackgroundColor:
                        BackgroundColor = Converter.GetColorFromString(value);
                        break;

                    case strPointColor:
                        PointColor = Converter.GetColorFromString(value);
                        break;

                    case strUseInfantGraphics:
                        UseInfantGraphics = bool.Parse(value);
                        break;

                    case strRandomizePointOrder:
                        RandomizePointOrder = bool.Parse(value);
                        break;

                    case strAutoAcceptPoints:
                        AutoAcceptPoints = bool.Parse(value);
                        break;

                    case strWaitForValidData:
                        WaitForValidData = bool.Parse(value);
                        break;
                }
            }
        }
    }
}