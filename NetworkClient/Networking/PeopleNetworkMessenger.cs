using System;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using NetworkLibrary.DataTransferObjects;
using NetworkLibrary.NetworkMessages;

namespace NetworkClient.Networking
{
    public class PeopleNetworkMessenger : INetworkMessenger<PersonDto>
    {
        private readonly NetworkWrapper _networkWrapper;
        private readonly UiDispatcher _uiDispatcher;

        public event EventHandler<MessageReceivedEventArgs<PersonDto>> DataReceived;

        public PeopleNetworkMessenger(NetworkWrapper networkWrapper, UiDispatcher uiDispatcher)
        {
            _networkWrapper = networkWrapper;
            _uiDispatcher = uiDispatcher;

            _networkWrapper.RegisterHandler<StorePersonMessage>(IncomingMessageHandler);
            _networkWrapper.RegisterHandler<AllPeopleMessage>(HandleAllPeople);

            _networkWrapper.SendObject(new AllPeopleMessage());
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
            _networkWrapper.SendObject(new StorePersonMessage(data));
        }
    }
}