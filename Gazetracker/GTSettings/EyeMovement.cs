using System;
using System.ComponentModel;
using System.Xml;

namespace GTSettings
{
    public class EyeMovement : INotifyPropertyChanged
    {
        #region Constants

        public const string Name = "EyeMovementSettings";

        #endregion

        #region Variables

        // Fixation detection
        private int distanceUserToScreen = 60; // In cm
        private int maxAngularSpeed = 50; // In degrees per second
        private int maxDispersion = 150;
        private int maxWindowSize = 20;

        // Smoothing
        private int smoothLevel = 50; // User selected, slider ranges from 1-100 
        private int smoothNumberOfSamples = 20;
        private int windowSize = 2;

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Get/Set

        public int MaxWindowSize
        {
            get { return maxWindowSize; }
            set
            {
                maxWindowSize = value;
                OnPropertyChanged("MaxWindowSize");
            }
        }

        public int WindowSize
        {
            get { return windowSize; }
            set
            {
                windowSize = value;
                OnPropertyChanged("WindowSize");
            }
        }

        public int DistanceUserToScreen
        {
            get { return distanceUserToScreen; }
            set
            {
                distanceUserToScreen = value;
                OnPropertyChanged("DistanceUserToScreen");
            }
        }

        public int MaxAngularSpeed
        {
            get { return maxAngularSpeed; }
            set
            {
                maxAngularSpeed = value;
                OnPropertyChanged("MaxAngularSpeed");
            }
        }

        public int MaxDispersion
        {
            get { return maxDispersion; }
            set
            {
                maxDispersion = value;
                OnPropertyChanged("MaxDispersion");
            }
        }

        // Smoothing

        public int SmoothLevel
        {
            get { return smoothLevel; }
            set
            {
                smoothLevel = value;
                OnPropertyChanged("SmoothLevel");
            }
        }

        public int SmoothNumberOfSamples
        {
            get { return smoothNumberOfSamples; }
            set
            {
                smoothNumberOfSamples = value;
                OnPropertyChanged("SmoothNumberOfSamples");
            }
        }

        #endregion

        #region Public Methods - Read/Write/encode configuration files

        public void WriteConfigFile(XmlTextWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("EyeMovementSettings");

            Settings.WriteElement(xmlWriter, "MaxWindowSize", MaxWindowSize.ToString());
            Settings.WriteElement(xmlWriter, "WindowSize", WindowSize.ToString());
            Settings.WriteElement(xmlWriter, "DistanceUserToScreen", DistanceUserToScreen.ToString());
            Settings.WriteElement(xmlWriter, "MaxAngularSpeed", MaxAngularSpeed.ToString());
            Settings.WriteElement(xmlWriter, "MaxDispersion", MaxDispersion.ToString());
            Settings.WriteElement(xmlWriter, "SmoothLevel", SmoothLevel.ToString());
            Settings.WriteElement(xmlWriter, "SmoothNumberOfSamples", SmoothNumberOfSamples.ToString());

            xmlWriter.WriteEndElement();
        }

        public void LoadConfigFile(XmlReader xmlReader)
        {
            string sName = "";

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
                            case "MaxWindowSize":
                                MaxWindowSize = int.Parse(xmlReader.Value);
                                break;
                            case "WindowSize":
                                WindowSize = int.Parse(xmlReader.Value);
                                break;
                            case "DistanceUserToScreen":
                                DistanceUserToScreen = int.Parse(xmlReader.Value);
                                break;
                            case "MaxAngularSpeed":
                                MaxAngularSpeed = int.Parse(xmlReader.Value);
                                break;
                            case "MaxDispersion":
                                MaxDispersion = int.Parse(xmlReader.Value);
                                break;
                            case "SmoothLevel":
                                SmoothLevel = Int32.Parse(xmlReader.Value);
                                break;
                            case "SmoothNumberOfSamples":
                                SmoothNumberOfSamples = int.Parse(xmlReader.Value);
                                break;
                        }
                        break;
                }
            }
        }

        #endregion

        #region EventHandler

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