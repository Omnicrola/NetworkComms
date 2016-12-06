using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NetworkClient.Annotations;
using NetworkLibrary.DataTransferObjects;
using NetworkLibrary.NetworkMessages;

namespace NetworkClient.Networking
{
    public class PeopleViewModel : INotifyPropertyChanged
    {
        private bool _isConnected;
        private string _lastResponse;

        public bool IsConnected
        {
            get { return _isConnected; }
            set
            {
                if (value == _isConnected) return;
                _isConnected = value;
                OnPropertyChanged();
            }
        }

        public string LastResponse
        {
            get { return _lastResponse; }
            set
            {
                if (value == _lastResponse) return;
                _lastResponse = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<PersonDto> People { get; set; }
        private NetworkManager _networkManager;

        public PeopleViewModel()
        {
            People = new ObservableCollection<PersonDto>();
            _networkManager = new NetworkManager();
            _networkManager.PersonAdded += Handle_AddPerson;
            People.Add(new PersonDto { FirstName = "Test", LastName = "Case" });
        }

        private void Handle_AddPerson(object sender, PersonAddedEvent e)
        {
            LastResponse += "Recieved person:" + e.Person.Id;
            People.Add(e.Person);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddPerson(PersonDto personDto)
        {
            LastResponse += _networkManager.SendMessage(new StorePersonMessage(personDto)) + "\n";
        }
    }
}