using System;
using System.ServiceModel;

namespace GTLibrary.Network
{
    public class CloudClient
    {

        #region Service contracts

        [ServiceContract]
        public interface ITransfer
        {
            [OperationContract]
            void Send(long id, string folder, TransferType transferType);

            [OperationContract]
            void UIVisibility(bool isVisibile);
        }

        #endregion


        #region Variables

        private static CloudClient instance;
        private ITransfer pipeProxy;
        private const string channelName = "GTCloudTransfer";


        public enum TransferType
        {
            Unspecified = 0,
            Calibration = 1,
            Sequence = 2,
        }


        #endregion


        #region Constructor

        private CloudClient()
        {
            pipeProxy = GetChannel(channelName).CreateChannel();
        }

        #endregion


        #region Public methods

        public bool Transfer(long id, string folderToArchive, TransferType transferType)
        {
            try
            {
                pipeProxy.Send(id, folderToArchive, transferType);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("GTCloudClient.cs, failed to call calibration transfer service. Message: " + ex.Message);
            }

            return true;
        }

        public void ShowSharingUI()
        {
            try
            {
                pipeProxy.UIVisibility(true);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("Error in GTCloudClient.cs, message: " + ex.Message);
            }

        }


        public void HideSharingUI()
        {
            try
            {
                pipeProxy.UIVisibility(false);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("Error in GTCloudClient.cs, message: " + ex.Message);
            }
        }

        #endregion


        #region Private methods

        private static ChannelFactory<ITransfer> GetChannel(string serviceName)
        {
            ChannelFactory<ITransfer> pipeFactory =
            new ChannelFactory<ITransfer>(
            new NetNamedPipeBinding(),
            new EndpointAddress("net.pipe://localhost/" + serviceName));
            return pipeFactory;
        }



        #endregion


        #region Get/Set

        public static CloudClient Instance
        {
            get
            {
                if (instance == null)
                    instance = new CloudClient();

                return instance;
            }
        }

        #endregion

    }
}
