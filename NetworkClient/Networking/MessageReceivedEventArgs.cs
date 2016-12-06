using System;

namespace NetworkClient.Networking
{
    public class MessageReceivedEventArgs<T> : EventArgs
    {
        public T Data { get; }

        public MessageReceivedEventArgs(T data)
        {
            Data = data;
        }
    }
}