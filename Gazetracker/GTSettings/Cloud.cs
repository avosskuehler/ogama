using System;
using System.ComponentModel;
using System.Xml;

namespace GTSettings
{
    public class Cloud : INotifyPropertyChanged
    {
        public const string Name = "CloudSettings";

        private bool isSharingCalibration = true;
        private bool isSharingUIVisible = false;


        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events


        #region Get/Set

        public bool IsSharingCalibrationData
        {
            get { return isSharingCalibration; }
            set
            {
                isSharingCalibration = value;
                OnPropertyChanged("IsSharingCalibration");
            }
        }

        public bool IsSharingUIVisible
        {
            get { return isSharingUIVisible; }
            set
            {
                isSharingUIVisible = value;
                OnPropertyChanged("IsSharingUIVisible");
            }
        }

        #endregion


        #region Public Methods

        public void WriteConfigFile(XmlTextWriter xmlWriter)
        {
            xmlWriter.WriteStartElement(Name);

            Settings.WriteElement(xmlWriter, "IsSharingCalibration", IsSharingCalibrationData.ToString());
            Settings.WriteElement(xmlWriter, "IsSharingUIVisible", IsSharingUIVisible.ToString());

            xmlWriter.WriteEndElement();
        }

        public void LoadConfigFile(XmlReader xmlReader)
        {
            string sName = string.Empty;

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
                            case "IsSharingCalibration":
                                IsSharingCalibrationData = Boolean.Parse(xmlReader.Value);
                                break;
                            case "IsSharingUIVisible":
                                IsSharingUIVisible = Boolean.Parse(xmlReader.Value);
                                break;
                        }
                        break;
                }
            }
        }

        #endregion //PUBLICMETHODS

        #region Private methods

        private void OnPropertyChanged(string parameter)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(parameter));
            }
        }

        #endregion //PRIVATEMETHODS


    }
}



