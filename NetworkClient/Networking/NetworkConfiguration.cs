using NetworkCommsDotNet;

namespace NetworkClient.Networking
{
    public class NetworkConfiguration
    {
        private readonly string _serverIp;
        private readonly int _serverPort;

        public NetworkConfiguration()
        {
            _serverIp = "127.0.0.1";
            _serverPort = 5400;
        }

        public void SendObject(object message)
        {
            NetworkComms.SendObject(message.GetType().Name, _serverIp, _serverPort, message);
        }
    }
}