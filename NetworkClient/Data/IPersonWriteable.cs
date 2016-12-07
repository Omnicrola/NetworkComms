using System;

namespace NetworkClient.Data
{
    public interface IPersonWriteable : IPerson
    {
        new int Id { get; set; }
        new string FirstName { get; set; }
        new string LastName { get; set; }
        new string Gender { get; set; }
        new DateTime Birthday { get; set; }
    }
}