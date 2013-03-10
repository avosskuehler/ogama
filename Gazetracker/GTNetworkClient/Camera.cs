namespace GTNetworkClient
{
    public class Camera
    {
        private readonly Client client;

        //private int _device;
        //private int _mode;


        public Camera(Client cli)
        {
            client = cli;
        }


        public int Brightness { get; set; }

        public int Contrast { get; set; }

        public int Saturation { get; set; }

        public int Sharpness { get; set; }

        public int Zoom { get; set; }

        public int Focus { get; set; }

        public int Exposure { get; set; }

        public void SetDeviceMode(int device, int mode)
        {
            client.SendCommand(Commands.CameraSetDeviceMode, device + "," + mode);
        }

        public void ApplyParameters()
        {
            client.SendCommand(Commands.CameraParameters, SettingsEncodeString());
        }

        public void ApplyParameters(string paramString)
        {
            client.SendCommand(Commands.CameraParameters, paramString);
        }


        private string SettingsEncodeString()
        {
            string sep = ",";
            string str = "";

            str += "Brightness=" + Brightness + sep;
            str += "Contrast=" + Contrast + sep;
            str += "Saturation=" + Saturation + sep;
            str += "Sharpness=" + Sharpness + sep;
            str += "Zoom=" + Zoom + sep;
            str += "Focus=" + Focus + sep;
            str += "Exposure=" + Exposure;

            return str;
        }
    }
}