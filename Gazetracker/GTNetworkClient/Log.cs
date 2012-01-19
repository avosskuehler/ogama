namespace GTNetworkClient
{
    public class Log
    {
        private readonly Client client;

        #region Events

        #region Delegates

        public delegate void PathChangeHandler(string newPath);

        public delegate void StartHandler();

        public delegate void StopHandler();

        #endregion

        public event StartHandler OnStart;

        public event StopHandler OnStop;

        public event PathChangeHandler OnPathChange;

        #endregion

        public Log(Client cli)
        {
            client = cli;
        }

        #region Commands Outgoing

        public void SetLogPath(string path)
        {
            client.SendCommand(Commands.LogPathSet, path);
        }

        public void GetLogPath()
        {
            client.SendCommand(Commands.LogPathGet);
        }

        public void WriteLine(string line)
        {
            client.SendCommand(Commands.LogWriteLine, line);
        }

        public void Start()
        {
            client.SendCommand(Commands.LogStart);
        }

        public void Stop()
        {
            client.SendCommand(Commands.LogStop);
        }

        #endregion

        #region Commands Incoming

        public void ExtractDataAndRaiseEvent(string data)
        {
            char[] seperator = {' '};
            string[] cmd = data.Split(seperator, 5);
            string command = cmd[0];
            string param = "";

            if (cmd.Length > 1)
                param = cmd[1];

            switch (command)
            {
                case Commands.LogStart:
                    if (OnStart != null)
                        OnStart();
                    break;
                case Commands.LogStop:
                    if (OnStop != null)
                        OnStop();
                    break;
                case Commands.LogPathGet:
                    if (OnPathChange != null)
                        OnPathChange(param);

                    break;
                case Commands.LogWriteLine:
                    // 
                    break;
            }
        }

        #endregion
    }
}