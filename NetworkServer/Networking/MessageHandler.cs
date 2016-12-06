using System;
using System.Linq;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using NetworkCommsDotNet.Connections.TCP;
using NetworkCommsDotNet.Tools;
using NetworkLibrary.NetworkMessages;

namespace NetworkServer.Networking
{
    public class MessageHandler
    {
        private DataStorage _dataStorage;

        public MessageHandler()
        {
            _dataStorage = new DataStorage();
        }

        public void Load()
        {
            NetworkComms.AppendGlobalIncomingPacketHandler<StorePersonMessage>(typeof(StorePersonMessage).Name, Handle_StorePerson);
            NetworkComms.AppendGlobalIncomingPacketHandler<GetAllPeopleMessage>(typeof(GetAllPeopleMessage).Name, Handle_GetAllPeople);
        }

        private void Handle_GetAllPeople(PacketHeader packetheader, Connection connection,
            GetAllPeopleMessage incomingobject)
        {
            connection.SendObject(typeof(AllPeopleMessage).Name, new AllPeopleMessage(_dataStorage.GetAllPeople().ToList()));
        }

        private void Handle_StorePerson(PacketHeader packetheader, Connection connection,
            StorePersonMessage incomingobject)
        {
            try
            {
                _dataStorage.Save(incomingobject.Person);
                Broadcast(new StorePersonMessage(incomingobject.Person));
            }
            catch (Exception e)
            {
                Console.WriteLine("EXCEPTION : " + e.GetType() + " " + e.Message);
            }
        }

        private void Broadcast(INetworkMessage message, ConnectionInfo excludeConnection = null)
        {
            try
            {
                NetworkComms.AllConnectionInfo()
                    .Where(c => c != excludeConnection)
                    .ToList()
                    .ForEach((connectionInfo) =>
                    {
                        try
                        {
                            var tcpConnection = TCPConnection.GetConnection(connectionInfo);
                            tcpConnection.SendObject(message.GetType().Name, message);
                        }
                        catch (CommsException e)
                        {
                            Console.WriteLine("Broadcast expception: " + e.Message);
                        }
                    });
            }
            catch (Exception e)
            {
                Console.WriteLine("EXCEPTION : " + e.GetType() + " " + e.Message);
            }
        }
    }
}