using System;
using NetworkCommsDotNet;

namespace NetworkClient.Networking
{
    public class NetworkWrapper : INetworkWrapper
    {
        private readonly string _serverIp;
        private readonly int _serverPort;

        public NetworkWrapper()
        {
            _serverIp = "127.0.0.1";
            _serverPort = 5400;
        }

        public void SendObject(object message)
        {
            try
            {
                NetworkComms.SendObject(message.GetType().Name, _serverIp, _serverPort, message);
            }
            catch (CommsException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void RegisterHandler<T>(NetworkComms.PacketHandlerCallBackDelegate<T> packetHandler)
        {
            NetworkComms.AppendGlobalIncomingPacketHandler<T>(typeof(T).Name,
               packetHandler);

        }
    }
}