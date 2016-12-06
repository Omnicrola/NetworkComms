using ProtoBuf;

namespace NetworkLibrary.NetworkMessages
{
    [ProtoContract]
    public class HelloMessage : INetworkMessage
    {
        [ProtoMember(1)]
        public string Message { get; set; }


        protected HelloMessage() { }
        public HelloMessage(string message)
        {
            Message = message;
        }
    }
}