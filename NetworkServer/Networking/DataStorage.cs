using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NetworkLibrary.DataTransferObjects;

namespace NetworkServer.Networking
{
    public class DataStorage
    {
        private readonly List<PersonDto> People = new List<PersonDto>();

        public void Save(PersonDto person)
        {
            Console.WriteLine("Storing: " + person);
            var match = People.FirstOrDefault(p => p.Id == person.Id);
            if (match == null)
            {
                People.Add(person);
            }
            else
            {
                match.FirstName = person.FirstName;
                match.LastName = person.LastName;
            }
        }

        public ReadOnlyCollection<PersonDto> GetAllPeople()
        {
            return People.AsReadOnly();
        }
    }
}