namespace GTNetworkClient
{
    public class Stream
    {
        private readonly Client client;
        private GazeData gazeData;
        private StreamFormat streamFormat;

        #region Events

        #region Delegates

        public delegate void DataHandler(string dataReceived);

        public delegate void FormatHandler(string formatStr);

        public delegate void StartHandler();

        public delegate void StopHandler();

        #endregion

        public event StartHandler OnStreamStart;

        public event StopHandler OnStreamStop;

        //public event FormatHandler OnStreamFormat;

        //public event DataHandler OnGazeData;

        #endregion

        public Stream(Client cli)
        {
            client = cli;
            gazeData = new GazeData();
        }

        public GazeData GazeData
        {
            get { return gazeData; }
            set { gazeData = value; }
        }

        public StreamFormat StreamFormat
        {
            get { return streamFormat; }
            set
            {
                streamFormat = value;

                if (streamFormat == null)
                    streamFormat = new StreamFormat();

                client.SendCommand(Commands.StreamFormat, "\"" + streamFormat.GetFormatString() + "\"");
            }
        }

        public void StreamStart()
        {
            client.SendCommand(Commands.StreamStart);
        }

        public void StreamStop()
        {
            client.SendCommand(Commands.StreamStop);
        }


        public void ExtractDataAndRaiseEvent(string data)
        {
            char[] seperator = {' '};
            string[] cmd = data.Split(seperator, 10);
            string command = cmd[0];

            switch (command)
            {
                case Commands.StreamStart:
                    if (OnStreamStart != null)
                        OnStreamStart();
                    break;

                case Commands.StreamStop:
                    if (OnStreamStop != null)
                        OnStreamStop();
                    break;

                case Commands.StreamData:
                    gazeData.extractTrackerData(streamFormat, data);
                    break;

                case Commands.StreamFormat:
                    streamFormat.Parse(data);
                    break;
            }
        }
    }
}