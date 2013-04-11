using System;
using System.ComponentModel;
using System.Drawing;
using System.Xml;
using GTCommons.Enum;

namespace GTSettings
{
    public class Visualization : INotifyPropertyChanged
    {
        #region Constants

        public const string Name = "VisualizationSettings";

        #endregion

        #region Variables

        private bool drawEyeROI = true;
        private bool drawEyesROI;
        private bool drawGlints;
        private bool drawPupil = true;

        private Color eyeROIColor = Color.DarkOrange;
        private int eyeROIGray;

        private Color glintCrossColor = Color.LightYellow;
        private int glintCrossGray = 255;
        private Color glintMaxColor = Color.DarkGray;
        private int glintMaxGray = 175;
        private Color glintMinColor = Color.Gray;
        private int glintMinGray = 175;
        private Color glintThresholdColor = Color.Yellow;
        private bool isDrawing = true;
        private Color pupilCrossColor = Color.Blue;

        private int pupilCrossGray;
        private Color pupilMaxColor = Color.DarkBlue;
        private int pupilMaxGray = 127;
        private Color pupilMinColor = Color.LightBlue;
        private int pupilMinGray = 127;
        private Color pupilThresholdColor = Color.Red;
        private VideoModeEnum videoMode = VideoModeEnum.Normal;


        //public enum ColorNames : int
        //{
        //    EyeROIColor = 1,
        //    PupilThresholdColor = 2,
        //    PupilCrossColor = 3,
        //    PupilMinColor = 4,
        //    PupilMaxColor = 5,
        //    GlintThresholdColor = 6,
        //    GlintCrossColor = 7,
        //    GlintMinColor = 8,
        //    GlintMaxColor = 9,
        //}

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion EVENTS

        #region Get/Set properties

        public VideoModeEnum VideoMode
        {
            get { return videoMode; }
            set
            {
                videoMode = value;
                OnPropertyChanged("VideoMode");
            }
        }

        public bool IsDrawing
        {
            get { return isDrawing; }
            set
            {
                isDrawing = value;
                OnPropertyChanged("IsDrawing");
            }
        }


        public bool DrawEyesROI
        {
            get { return drawEyesROI; }
            set
            {
                drawEyesROI = value;
                OnPropertyChanged("DrawEyesROI");
            }
        }

        #region Eye ROI

        public bool DrawEyeROI
        {
            get { return drawEyeROI; }
            set
            {
                drawEyeROI = value;
                OnPropertyChanged("DrawEyeROI");
            }
        }

        public Color EyeROIColor
        {
            get { return eyeROIColor; }
            set
            {
                eyeROIColor = value;
                OnPropertyChanged("EyeROIColor");
            }
        }

        public int EyeROIGray
        {
            get { return eyeROIGray; }
            set
            {
                eyeROIGray = value;
                OnPropertyChanged("EyeROIGray");
            }
        }

        #endregion

        #region Pupil

        public bool DrawPupil
        {
            get { return drawPupil; }
            set
            {
                drawPupil = value;
                OnPropertyChanged("DrawPupil");
            }
        }

        public Color PupilThresholdColor
        {
            get { return pupilThresholdColor; }
            set
            {
                pupilThresholdColor = value;
                OnPropertyChanged("PupilThresholdColor");
            }
        }

        public Color PupilCrossColor
        {
            get { return pupilCrossColor; }
            set
            {
                pupilCrossColor = value;
                OnPropertyChanged("PupilCrossColor");
            }
        }

        public Color PupilMinColor
        {
            get { return pupilMinColor; }
            set
            {
                pupilMinColor = value;
                OnPropertyChanged("PupilMinColor");
            }
        }

        public Color PupilMaxColor
        {
            get { return pupilMaxColor; }
            set
            {
                pupilMaxColor = value;
                OnPropertyChanged("PupilMaxColor");
            }
        }

        public int PupilCrossGray
        {
            get { return pupilCrossGray; }
            set
            {
                pupilCrossGray = value;
                OnPropertyChanged("PupilCrossGray");
            }
        }

        public int PupilMinGray
        {
            get { return pupilMinGray; }
            set
            {
                pupilMinGray = value;
                OnPropertyChanged("PupilMinGray");
            }
        }

        public int PupilMaxGray
        {
            get { return pupilMaxGray; }
            set
            {
                pupilMaxGray = value;
                OnPropertyChanged("PupilMaxGray");
            }
        }

        #endregion

        #region Glints

        public bool DrawGlints
        {
            get { return drawGlints; }
            set
            {
                drawGlints = value;
                OnPropertyChanged("DrawGlints");
            }
        }

        public Color GlintThresholdColor
        {
            get { return glintThresholdColor; }
            set
            {
                glintThresholdColor = value;
                OnPropertyChanged("GlintThresholdColor");
            }
        }

        public Color GlintCrossColor
        {
            get { return glintCrossColor; }
            set
            {
                glintCrossColor = value;
                OnPropertyChanged("GlintCrossColor");
            }
        }

        public Color GlintMinColor
        {
            get { return glintMinColor; }
            set
            {
                glintMinColor = value;
                OnPropertyChanged("GlintMinColor");
            }
        }

        public Color GlintMaxColor
        {
            get { return glintMaxColor; }
            set
            {
                glintMaxColor = value;
                OnPropertyChanged("GlintMaxColor");
            }
        }

        public int GlintCrossGray
        {
            get { return glintCrossGray; }
            set
            {
                glintCrossGray = value;
                OnPropertyChanged("GlintCrossGray");
            }
        }

        public int GlintMinGray
        {
            get { return glintMinGray; }
            set
            {
                glintMinGray = value;
                OnPropertyChanged("GlintMinGray");
            }
        }

        public int GlintMaxGray
        {
            get { return glintMaxGray; }
            set
            {
                glintMaxGray = value;
                OnPropertyChanged("GlintMaxGray");
            }
        }

        #endregion

        #endregion // Properties

        #region Public methods

        public void WriteConfigFile(XmlTextWriter xmlWriter)
        {
            xmlWriter.WriteStartElement(Name);

            Settings.WriteElement(xmlWriter, "VideoMode", Enum.GetName(typeof (VideoModeEnum), videoMode));
            Settings.WriteElement(xmlWriter, "DrawEyeROI", DrawEyeROI.ToString());
            Settings.WriteElement(xmlWriter, "DrawPupil", DrawPupil.ToString());
            Settings.WriteElement(xmlWriter, "DrawGlints", DrawGlints.ToString());
            Settings.WriteElement(xmlWriter, "PupilThresholdColor", ColorToString(PupilThresholdColor));
            Settings.WriteElement(xmlWriter, "PupilCrossColor", ColorToString(PupilCrossColor));
            Settings.WriteElement(xmlWriter, "PupilMinColor", ColorToString(PupilMinColor));
            Settings.WriteElement(xmlWriter, "PupilMaxColor", ColorToString(PupilMaxColor));
            Settings.WriteElement(xmlWriter, "PupilCrossGray", PupilCrossGray.ToString());
            Settings.WriteElement(xmlWriter, "PupilMinGray", PupilMinGray.ToString());
            Settings.WriteElement(xmlWriter, "PupilMaxGray", PupilMaxGray.ToString());
            Settings.WriteElement(xmlWriter, "GlintThresholdColor", ColorToString(GlintThresholdColor));
            Settings.WriteElement(xmlWriter, "GlintCrossColor", ColorToString(GlintCrossColor));
            Settings.WriteElement(xmlWriter, "GlintMinColor", ColorToString(GlintMinColor));
            Settings.WriteElement(xmlWriter, "GlintMaxColor", ColorToString(GlintMaxColor));
            Settings.WriteElement(xmlWriter, "GlintCrossGray", GlintCrossGray.ToString());
            Settings.WriteElement(xmlWriter, "GlintMinGray", GlintMinGray.ToString());
            Settings.WriteElement(xmlWriter, "GlintMaxGray", GlintMaxGray.ToString());

            xmlWriter.WriteEndElement();
        }

        public void LoadConfigFile(XmlReader xmlReader)
        {
            string sName = string.Empty;

            if (xmlReader != null)
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
                                case "VideoMode":
                                    VideoMode =
                                        (VideoModeEnum) Enum.Parse(typeof (VideoModeEnum), xmlReader.Value, true);
                                    break;

                                case "DrawEyeROI":
                                    drawEyeROI = bool.Parse(xmlReader.Value);
                                    break;
                                case "DrawPupil":
                                    drawPupil = bool.Parse(xmlReader.Value);
                                    break;
                                case "DrawGlints":
                                    drawGlints = bool.Parse(xmlReader.Value);
                                    break;

                                case "EyeROIColor":
                                    EyeROIColor = ColorFromString(xmlReader.Value);
                                    break;
                                case "EyeROIGray":
                                    EyeROIGray = int.Parse(xmlReader.Value);
                                    break;

                                case "PupilThresholdColor":
                                    PupilThresholdColor = ColorFromString(xmlReader.Value);
                                    break;
                                case "PupilCrossColor":
                                    PupilCrossColor = ColorFromString(xmlReader.Value);
                                    break;
                                case "PupilMinColor":
                                    PupilMinColor = ColorFromString(xmlReader.Value);
                                    break;
                                case "PupilMaxColor":
                                    PupilMaxColor = ColorFromString(xmlReader.Value);
                                    break;
                                case "PupilCrossGray":
                                    PupilCrossGray = int.Parse(xmlReader.Value);
                                    break;
                                case "PupilMinGray":
                                    PupilMinGray = int.Parse(xmlReader.Value);
                                    break;
                                case "PupilMaxGray":
                                    PupilMaxGray = int.Parse(xmlReader.Value);
                                    break;

                                case "GlintThresholdColor":
                                    GlintThresholdColor = ColorFromString(xmlReader.Value);
                                    break;
                                case "GlintCrossColor":
                                    GlintCrossColor = ColorFromString(xmlReader.Value);
                                    break;
                                case "GlintMinColor":
                                    GlintMinColor = ColorFromString(xmlReader.Value);
                                    break;
                                case "GlintMaxColor":
                                    GlintMaxColor = ColorFromString(xmlReader.Value);
                                    break;
                                case "GlintCrossGray":
                                    GlintCrossGray = int.Parse(xmlReader.Value);
                                    break;
                                case "GlintMinGray":
                                    GlintMinGray = int.Parse(xmlReader.Value);
                                    break;
                                case "GlintMaxGray":
                                    GlintMaxGray = int.Parse(xmlReader.Value);
                                    break;
                            }
                            break;
                    }
                }
        }

        #endregion //PUBLICMETHODS

        #region Eventhandler

        private void OnPropertyChanged(string parameter)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(parameter));
            }
        }

        #endregion //EVENTHANDLER

        #region Helpers

        public Color ColorFromString(string str)
        {
            string[] td = str.Split(' ');
            var bytes = new byte[td.Length];
            int i = 0;

            foreach (string s in td)
            {
                try
                {
                    if (bytes != null) bytes.SetValue(byte.Parse(s), i);
                    i++;
                }
                catch (Exception ex)
                {
                    bytes = null;
                    Console.Out.WriteLine("Error in GetColorFromString: " + ex.Message);
                }
            }

            if (bytes != null)
                return Color.FromArgb(0, bytes[0], bytes[1], bytes[2]);

            return new Color();
        }

        public static string ColorToString(Color color)
        {
            return color.R + " " + color.G + " " + color.B;
        }

        #endregion
    }
}