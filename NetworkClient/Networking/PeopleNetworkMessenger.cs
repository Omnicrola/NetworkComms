using System;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using NetworkLibrary.DataTransferObjects;
using NetworkLibrary.NetworkMessages;

namespace NetworkClient.Networking
{
    public class PeopleNetworkMessenger : INetworkMessenger<PersonDto>
    {
        private readonly NetworkConfiguration _networkConfiguration;
        private readonly UiDispatcher _uiDispatcher;

        public event EventHandler<MessageReceivedEventArgs<PersonDto>> DataReceived;

        public PeopleNetworkMessenger(NetworkConfiguration networkConfiguration, UiDispatcher uiDispatcher)
        {
            _networkConfiguration = networkConfiguration;
            _uiDispatcher = uiDispatcher;
            NetworkComms.AppendGlobalIncomingPacketHandler<StorePersonMessage>(typeof(StorePersonMessage).Name,
                IncomingMessageHandler);
        }

        private void IncomingMessageHandler(PacketHeader packetheader, Connection connection,
            StorePersonMessage incomingobject)
        {
            _uiDispatcher.Dispatch(
                () =>
                {
                    var messageReceivedEventArgs = new MessageReceivedEventArgs<PersonDto>(incomingobject.Person);
                    DataReceived?.Invoke(this, messageReceivedEventArgs);
                });
        }

        public void Update(PersonDto data)
        {
            _networkConfiguration.SendObject(new StorePersonMessage(data));
        }
    }
}