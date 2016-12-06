using ProtoBuf;

namespace NetworkLibrary.NetworkMessages
{
    [ProtoContract]
    public class GetAllPeopleMessage
    {
        public static string Type => typeof(GetAllPeopleMessage).Name;
    }
}