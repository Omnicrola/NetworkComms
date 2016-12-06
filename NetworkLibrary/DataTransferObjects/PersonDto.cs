using ProtoBuf;

namespace NetworkLibrary.DataTransferObjects
{
    [ProtoContract]
    public class PersonDto
    {
        [ProtoMember(1)]
        public string FirstName { get; set; }
        [ProtoMember(2)]
        public string LastName { get; set; }
        [ProtoMember(3)]
        public int Id { get; set; }

    }
}