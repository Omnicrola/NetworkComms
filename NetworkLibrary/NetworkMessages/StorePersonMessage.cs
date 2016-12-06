using NetworkLibrary.DataTransferObjects;
using ProtoBuf;

namespace NetworkLibrary.NetworkMessages
{
    [ProtoContract]
    public class StorePersonMessage : INetworkMessage
    {
        [ProtoMember(1)]
        public PersonDto Person { get; set; }

        public static string Type => typeof(StorePersonMessage).Name;

        protected StorePersonMessage()
        {
        }

        public StorePersonMessage(PersonDto person)
        {
            Person = person;
        }
    }
}