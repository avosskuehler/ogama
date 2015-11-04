namespace GTNetworkClient
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Net;
    using System.Xml;

    using GTCommons;

    public class Settings : INotifyPropertyChanged
    {

        #region Variables

        private string directory;
        private IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        //private FileSystemWatcher myWatcher;
        private string settingsFileName = "GazeTrackerSettings.xml";
        private bool settingsLoaded;

        private int tcpIpServerPort = 5555; // default
        private int udpServerPort = 6666; // default
        private XmlTextWriter xmlWriter;

        #endregion

        #region Constructor

        public Settings()
        {
            // Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            this.directory = GTPath.GetLocalApplicationDataPath();
            this.LoadSettings();
        }

        #endregion

        #region Get/Set properties

        public string SettingsDirectory
        {
            get { return directory; }
            set { directory = value; }
        }

        public IPAddress IPAddress
        {
            get { return ipAddress; }
            set
            {
                ipAddress = value;
                if (null != PropertyChanged)
                    PropertyChanged(this, new PropertyChangedEventArgs("IPAddress"));
            }
        }

        public int TCPIPServerPort
        {
            get { return tcpIpServerPort; }
            set
            {
                tcpIpServerPort = value;
                if (null != PropertyChanged)
                    PropertyChanged(this, new PropertyChangedEventArgs("TCPIPServerPort"));
            }
        }

        public int UDPServerPort
        {
            get { return udpServerPort; }
            set
            {
                udpServerPort = value;
                if (null != PropertyChanged)
                    PropertyChanged(this, new PropertyChangedEventArgs("UDPServerPort"));
            }
        }

        public bool SettingsLoaded
        {
            get { return settingsLoaded; }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private void LoadSettings()
        {
            var dir = new Uri(directory);
            var dirInfo = new DirectoryInfo(dir.LocalPath);

            foreach (FileInfo file in dirInfo.GetFiles("*.xml"))
            {
                if (file.Name.Equals(settingsFileName))
                    LoadConfigFile(file.FullName);
            }
        }

        public void WriteConfigFile()
        {
            try
            {
                var fileUri = new Uri(directory + Path.DirectorySeparatorChar + settingsFileName);
                xmlWriter = new XmlTextWriter(fileUri.LocalPath, null);
                xmlWriter.Formatting = Formatting.Indented;
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("GazeTrackerClientSettings");

                WriteElement("IPAddress", IPAddress.ToString());
                WriteElement("TCPIPServerPort", TCPIPServerPort.ToString());
                WriteElement("UDPServerPort", UDPServerPort.ToString());

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
                xmlWriter.Flush();
                xmlWriter.Close();
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("Error saving configuration xml:" + ex.Message);
            }
        }

        private void WriteElement(string key, string value)
        {
            xmlWriter.WriteStartElement(key);
            xmlWriter.WriteString(value);
            xmlWriter.WriteEndElement();
        }

        public void LoadConfigFile(string filename)
        {
            if (filename.Length < 1)
            {
                settingsLoaded = false;
            }
            else
            {
                try
                {
                    var fileUri = new Uri(directory + Path.DirectorySeparatorChar + settingsFileName);
                    var settingsFile = new FileInfo(fileUri.LocalPath);
                    var xmlReader = new XmlTextReader(settingsFile.FullName);
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
                                    case "IPAddress":
                                        this.IPAddress = IPAddress.Parse(xmlReader.Value);
                                        break;
                                    case "TCPIPServerPort":
                                        this.TCPIPServerPort = int.Parse(xmlReader.Value);
                                        break;
                                    case "UDPServerPort":
                                        this.UDPServerPort = int.Parse(xmlReader.Value);
                                        break;
                                }

                                break;
                        }

                        this.settingsLoaded = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.Out.WriteLine("Error loading config: " + ex.Message);
                }
            }
        }
    }
}