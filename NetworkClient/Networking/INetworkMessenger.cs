using System;
using NetworkClient.Data;

namespace NetworkClient.Networking
{
    public interface INetworkMessenger<T>
    {
        void Update(T data);
        event EventHandler<MessageReceivedEventArgs<T>> DataReceived;
    }
}