using System.ComponentModel;
using System.Xml;

namespace GTSettings
{
    public class Autotune : INotifyPropertyChanged
    {
        #region Constants

        public const string Name = "AutotuneSettings";

        #endregion

        #region Variables

        private int eyeMaxAttempts = 3;
        private int eyeMaxFrameCount = 100;
        private double eyeROIMaxVariationHeight = 10;
        private double eyeROIMaxVariationWidth = 10;

        private int eyesMaxAttempts = 3;

        private int eyesMaxFrameCount = 50;
        private double eyesROIMaxVariationWidth = 30;
        private int glintMaxAttempts = 3;
        private int glintMaxFrameCount = 100;
        private int pupilMaxAttempts = 3;
        private int pupilMaxFrameCount = 100;
        private double pupilMaxVariationArea = 50;
        private double pupilMaxVariationCentroid = 5;
        private double pupilMaxVariationDistance = 5;

        public enum AutotuneCheckLevel
        {
            Low = 1,
            Medium = 2,
            Hight = 3,
        }

        #endregion

        #region Events 

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Get/Set

        #region Min/max optimal values

        public double EyesROIMaxVariationWidth
        {
            get { return eyesROIMaxVariationWidth; }
            set
            {
                eyesROIMaxVariationWidth = value;
                OnPropertyChanged("EyesROIMaxVariationWidth");
            }
        }

        public double EyeROIMaxVariationWidth
        {
            get { return eyeROIMaxVariationWidth; }
            set
            {
                eyeROIMaxVariationWidth = value;
                OnPropertyChanged("EyeROIMaxVariationWidth");
            }
        }

        public double EyeROIMaxVariationHeight
        {
            get { return eyeROIMaxVariationHeight; }
            set
            {
                eyeROIMaxVariationHeight = value;
                OnPropertyChanged("EyeROIMaxVariationHeight");
            }
        }

        public double PupilMaxVariationArea
        {
            get { return pupilMaxVariationArea; }
            set
            {
                pupilMaxVariationArea = value;
                OnPropertyChanged("PupilMaxVariationArea");
            }
        }

        public double PupilMaxVariationCentroid
        {
            get { return pupilMaxVariationCentroid; }
            set
            {
                pupilMaxVariationCentroid = value;
                OnPropertyChanged("PupilMaxVariationCentroid");
            }
        }

        public double PupilMaxVariationDistance
        {
            get { return pupilMaxVariationDistance; }
            set
            {
                pupilMaxVariationDistance = value;
                OnPropertyChanged("PupilMaxVariationDistance");
            }
        }

        #endregion

        #region Max frame counts 

        public int EyesMaxFrameCount
        {
            get { return eyesMaxFrameCount; }
            set
            {
                eyesMaxFrameCount = value;
                OnPropertyChanged("EyesMaxFrameCount");
            }
        }

        public int EyeMaxFrameCount
        {
            get { return eyeMaxFrameCount; }
            set
            {
                eyeMaxFrameCount = value;
                OnPropertyChanged("EyeMaxFrameCount");
            }
        }

        public int PupilMaxFrameCount
        {
            get { return pupilMaxFrameCount; }
            set
            {
                pupilMaxFrameCount = value;
                OnPropertyChanged("PupilMaxFrameCount");
            }
        }

        public int GlintMaxFrameCount
        {
            get { return glintMaxFrameCount; }
            set
            {
                glintMaxFrameCount = value;
                OnPropertyChanged("GlintMaxFrameCount");
            }
        }

        #endregion

        #region Max attempts

        public int EyesMaxAttempts
        {
            get { return eyesMaxAttempts; }
            set
            {
                eyesMaxAttempts = value;
                OnPropertyChanged("EyesMaxAttempts");
            }
        }

        public int EyeMaxAttempts
        {
            get { return eyeMaxAttempts; }
            set
            {
                eyeMaxAttempts = value;
                OnPropertyChanged("EyeMaxAttempts");
            }
        }

        public int PupilMaxAttempts
        {
            get { return pupilMaxAttempts; }
            set
            {
                pupilMaxAttempts = value;
                OnPropertyChanged("PupilMaxAttempts");
            }
        }

        public int GlintMaxAttempts
        {
            get { return glintMaxAttempts; }
            set
            {
                glintMaxAttempts = value;
                OnPropertyChanged("GlintMaxAttempts");
            }
        }

        #endregion

        #endregion

        #region Public Methods - Read/Write/encode configuration files

        public void WriteConfigFile(XmlTextWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("AutotuneSettings");

            Settings.WriteElement(xmlWriter, "eyesROIMaxVariationWidth", eyesROIMaxVariationWidth.ToString());
            Settings.WriteElement(xmlWriter, "EyeROIMaxVariationWidth", EyeROIMaxVariationWidth.ToString());
            Settings.WriteElement(xmlWriter, "EyeROIMaxVariationHeight", EyeROIMaxVariationHeight.ToString());
            Settings.WriteElement(xmlWriter, "PupilMaxVariationArea", PupilMaxVariationArea.ToString());
            Settings.WriteElement(xmlWriter, "PupilMaxVariationCentroid", PupilMaxVariationCentroid.ToString());
            Settings.WriteElement(xmlWriter, "PupilMaxVariationDistance", PupilMaxVariationDistance.ToString());

            Settings.WriteElement(xmlWriter, "eyesMaxFrameCount", eyesMaxFrameCount.ToString());
            Settings.WriteElement(xmlWriter, "EyeMaxFrameCount", EyeMaxFrameCount.ToString());
            Settings.WriteElement(xmlWriter, "PupilMaxFrameCount", PupilMaxFrameCount.ToString());
            Settings.WriteElement(xmlWriter, "GlintMaxFrameCount", GlintMaxFrameCount.ToString());

            Settings.WriteElement(xmlWriter, "eyesMaxAttempts", eyesMaxFrameCount.ToString());
            Settings.WriteElement(xmlWriter, "EyeMaxAttempts", EyeMaxFrameCount.ToString());
            Settings.WriteElement(xmlWriter, "PupilMaxAttempts", PupilMaxFrameCount.ToString());
            Settings.WriteElement(xmlWriter, "GlintMaxAttempts", GlintMaxFrameCount.ToString());

            xmlWriter.WriteEndElement();
        }

        public void LoadConfigFile(XmlReader xmlReader)
        {
            string sName = "";
            //int min;
            //int max;

            while (xmlReader.Read())
            {
                switch (xmlReader.NodeType)
                {
                    case XmlNodeType.Element:
                        sName = xmlReader.Name;
                        break;
                    case XmlNodeType.Text:
                        switch (sName)
                        {
                            case "eyesROIMaxVariationWidth":
                                eyesROIMaxVariationWidth = double.Parse(xmlReader.Value);
                                break;
                            case "EyeROIMaxVariationWidth":
                                EyeROIMaxVariationWidth = double.Parse(xmlReader.Value);
                                break;
                            case "EyeROIMaxVariationHeight":
                                EyeROIMaxVariationHeight = double.Parse(xmlReader.Value);
                                break;
                            case "PupilMaxVariationArea":
                                PupilMaxVariationArea = double.Parse(xmlReader.Value);
                                break;
                            case "PupilMaxVariationCentroid":
                                PupilMaxVariationCentroid = double.Parse(xmlReader.Value);
                                break;
                            case "PupilMaxVariationDistance":
                                PupilMaxVariationDistance = double.Parse(xmlReader.Value);
                                break;

                            case "eyesMaxFrameCount":
                                eyesMaxFrameCount = int.Parse(xmlReader.Value);
                                break;
                            case "EyeMaxFrameCount":
                                EyeMaxFrameCount = int.Parse(xmlReader.Value);
                                break;
                            case "PupilMaxFrameCount":
                                PupilMaxFrameCount = int.Parse(xmlReader.Value);
                                break;
                            case "GlintMaxFrameCount":
                                GlintMaxFrameCount = int.Parse(xmlReader.Value);
                                break;

                            case "eyesMaxAttempts":
                                eyesMaxAttempts = int.Parse(xmlReader.Value);
                                break;
                            case "EyeMaxAttempts":
                                EyeMaxAttempts = int.Parse(xmlReader.Value);
                                break;
                            case "PupilMaxAttempts":
                                PupilMaxAttempts = int.Parse(xmlReader.Value);
                                break;
                            case "GlintMaxAttempts":
                                GlintMaxAttempts = int.Parse(xmlReader.Value);
                                break;
                        }
                        break;
                }
            }
        }

        public void ApplyDefaults(AutotuneCheckLevel checkLevel)
        {
            switch (checkLevel)
            {
                case AutotuneCheckLevel.Low:

                    eyesROIMaxVariationWidth = 75;
                    EyeROIMaxVariationWidth = 30;
                    EyeROIMaxVariationHeight = 30;
                    PupilMaxVariationArea = 100;
                    PupilMaxVariationCentroid = 15;
                    PupilMaxVariationDistance = 15;

                    eyesMaxAttempts = 1;
                    EyeMaxAttempts = 1;
                    PupilMaxAttempts = 1;
                    GlintMaxAttempts = 1;

                    eyesMaxFrameCount = 10;
                    EyeMaxFrameCount = 25;
                    PupilMaxFrameCount = 35;
                    GlintMaxFrameCount = 35;

                    break;

                case AutotuneCheckLevel.Medium:

                    eyesROIMaxVariationWidth = 50;
                    EyeROIMaxVariationWidth = 20;
                    EyeROIMaxVariationHeight = 20;
                    PupilMaxVariationArea = 75;
                    PupilMaxVariationCentroid = 7;
                    PupilMaxVariationDistance = 10;

                    eyesMaxAttempts = 2;
                    EyeMaxAttempts = 2;
                    PupilMaxAttempts = 2;
                    GlintMaxAttempts = 2;

                    eyesMaxFrameCount = 25;
                    EyeMaxFrameCount = 50;
                    PupilMaxFrameCount = 50;
                    GlintMaxFrameCount = 50;

                    break;

                case AutotuneCheckLevel.Hight:

                    eyesROIMaxVariationWidth = 30;
                    EyeROIMaxVariationWidth = 10;
                    EyeROIMaxVariationHeight = 10;
                    PupilMaxVariationArea = 50;
                    PupilMaxVariationCentroid = 3;
                    PupilMaxVariationDistance = 5;

                    eyesMaxAttempts = 3;
                    EyeMaxAttempts = 3;
                    PupilMaxAttempts = 3;
                    GlintMaxAttempts = 3;

                    eyesMaxFrameCount = 50;
                    EyeMaxFrameCount = 100;
                    PupilMaxFrameCount = 100;
                    GlintMaxFrameCount = 100;

                    break;
            }
        }


        public string SettingsEncodeString()
        {
            //string sep = ",";
            string str = "";

            //str += "Brightness=" + Brightness.ToString() + sep;
            //str += "Contrast=" + Contrast.ToString() + sep;
            //str += "Saturation=" + Saturation.ToString() + sep;
            //str += "Sharpness=" + Sharpness.ToString() + sep;
            //str += "Zoom=" + Zoom.ToString() + sep;
            //str += "Focus=" + Focus.ToString() + sep;
            //str += "Exposure=" + Exposure.ToString() + sep;
            //str += "FlipImage=" + FlipImage.ToString();

            return str;
        }

        public void ExtractParametersFromString(string parameterStr)
        {
            //try
            //{
            //    // Seperating commands
            //    char[] sepParam = { ',' };
            //    string[] camParams = parameterStr.Split(sepParam, 20);

            //    // Seperating values/parameters
            //    char[] sepValues = { '=' };

            //    for (int i = 0; i < camParams.Length; i++)
            //    {
            //        string[] cmdString = camParams[i].Split(sepValues, 5);
            //        string subCmdStr = cmdString[0];
            //        string value = cmdString[1];

            //        switch (subCmdStr)
            //        {
            //            case "Brightness":
            //                this.Brightness = int.Parse(value);
            //                break;
            //            case "Contrast":
            //                this.Contrast = int.Parse(value);
            //                break;
            //            case "Saturation":
            //                this.Saturation = int.Parse(value);
            //                break;
            //            case "Sharpness":
            //                this.Sharpness = int.Parse(value);
            //                break;
            //            case "Zoom":
            //                this.Zoom = int.Parse(value);
            //                break;
            //            case "Focus":
            //                this.Focus = int.Parse(value);
            //                break;
            //            case "Exposure":
            //                this.Exposure = int.Parse(value);
            //                break;
            //            case "FlipImage":
            //                this.FlipImage = bool.Parse(value);
            //                break;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //}
        }

        #endregion

        #region EVENTHANDLER

        private void OnPropertyChanged(string parameter)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(parameter));
            }
        }

        #endregion //EVENTHANDLER
    }
}