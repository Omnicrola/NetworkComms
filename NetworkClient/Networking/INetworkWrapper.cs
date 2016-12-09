using NetworkCommsDotNet;

namespace NetworkClient.Networking
{
    public interface INetworkWrapper
    {
        void SendObject(object message);
        void RegisterHandler<T>(NetworkComms.PacketHandlerCallBackDelegate<T> packetHandler);
    }
}