using NetworkClient.Networking;
using NetworkLibrary.DataTransferObjects;

namespace NetworkClient.Data
{
    public class MasterDataSource
    {
        public PeopleDataSource PeopleDataSource { get; }


        private static MasterDataSource _instance = null;

        public static MasterDataSource Instance()
        {
            if (_instance == null)
            {
                _instance = new MasterDataSource();
            }
            return _instance;
        }

        private MasterDataSource()
        {
            var networkConfiguration = new NetworkConfiguration();
            var uiDispatcher = new UiDispatcher();
            var peopleNetworkMessenger = new PeopleNetworkMessenger(networkConfiguration, uiDispatcher);
            PeopleDataSource = new PeopleDataSource(peopleNetworkMessenger);
        }
    }
}