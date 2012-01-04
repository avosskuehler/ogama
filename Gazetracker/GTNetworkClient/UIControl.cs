namespace GTNetworkClient
{
    public class UIControl
    {
        private readonly Client client;


        public UIControl(Client cli)
        {
            client = cli;
        }

        public void Minimize()
        {
            client.SendCommand(Commands.UIMinimize);
        }

        public void Restore()
        {
            client.SendCommand(Commands.UIRestore);
        }

        public void Settings()
        {
            client.SendCommand(Commands.UISettings);
        }

        public void SettingsCamera()
        {
            client.SendCommand(Commands.UISettingsCamera);
        }

        public void ExtractDataAndRaiseEvent(string data)
        {
        }
    }
}