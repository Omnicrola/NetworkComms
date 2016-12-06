using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NetworkClient.Networking;
using NetworkLibrary.DataTransferObjects;

namespace NetworkClient.Data
{
    public class PeopleDataSource : DataSource<IPerson>
    {
        private readonly PeopleNetworkMessenger _peopleNetworkMessenger;
        private List<NetworkedPerson> _writeablePeople;
        private ObservableCollection<IPerson> _people;
        public override DataCollection<IPerson> Data { get; }


        public PeopleDataSource(PeopleNetworkMessenger peopleNetworkMessenger)
        {
            _peopleNetworkMessenger = peopleNetworkMessenger;
            _writeablePeople = new List<NetworkedPerson>();
            _people = new ObservableCollection<IPerson>();
            Data = new DataCollection<IPerson>(_people);

            _peopleNetworkMessenger.DataReceived += UpdateData;
        }

        private void UpdateData(object sender, MessageReceivedEventArgs<PersonDto> eventArgs)
        {
            var updatedPerson = eventArgs.Data;
            var foundPerson = _writeablePeople.FirstOrDefault(p => p.Id == updatedPerson.Id);
            if (foundPerson == null)
            {
                var newPerson = new NetworkedPerson(_peopleNetworkMessenger)
                {
                    Id = updatedPerson.Id,
                    FirstName = updatedPerson.FirstName,
                    LastName = updatedPerson.LastName
                };
                _writeablePeople.Add(newPerson);
                _people.Add(newPerson);
            }
            else
            {
                foundPerson.FirstName = updatedPerson.FirstName;
                foundPerson.LastName = updatedPerson.LastName;
            }
        }

        public override IPerson Create()
        {
            return new NetworkedPerson(_peopleNetworkMessenger) { Id = -1 };
        }
    }
}