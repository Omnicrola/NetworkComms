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
            NetworkComms.AppendGlobalIncomingPacketHandler<AllPeopleMessage>(typeof(AllPeopleMessage).Name,
                HandleAllPeople);
            _networkConfiguration.SendObject(new AllPeopleMessage());
        }

        private void HandleAllPeople(PacketHeader packetheader, Connection connection, AllPeopleMessage incomingobject)
        {
            Console.WriteLine("Message recieved: AllPeopleMessage");
            _uiDispatcher.Dispatch(() =>
            {
                foreach (PersonDto personDto in incomingobject.People)
                {
                    var messageReceivedEventArgs = new MessageReceivedEventArgs<PersonDto>(personDto);
                    DataReceived?.Invoke(this, messageReceivedEventArgs);
                }
            });
        }

        private void IncomingMessageHandler(PacketHeader packetheader, Connection connection,
            StorePersonMessage incomingobject)
        {
            Console.WriteLine("Message recieved: StorePerson");
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