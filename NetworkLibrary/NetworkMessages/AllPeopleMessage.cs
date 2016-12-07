using System.Collections.Generic;
using NetworkLibrary.DataTransferObjects;
using ProtoBuf;

namespace NetworkLibrary.NetworkMessages
{
    [ProtoContract]
    public class AllPeopleMessage
    {
        [ProtoMember(1)]
        public List<PersonDto> People { get; set; }

        public AllPeopleMessage() { }
        public AllPeopleMessage(List<PersonDto> people)
        {
            People = people;
        }
    }
}