using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NetworkClient.Annotations;
using NetworkClient.Networking;
using NetworkLibrary.DataTransferObjects;

namespace NetworkClient.Data
{
    public class NetworkedPerson : IPersonWriteable
    {
        private readonly PeopleNetworkMessenger _networkMessenger;
        private int _id;
        private string _firstName;
        private string _lastName;
        private string _gender;
        private DateTime _birthday;

        public int Id
        {
            get { return _id; }
            set
            {
                if (value == _id) return;
                _id = value;
                OnPropertyChanged();
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (value == _firstName) return;
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (value == _lastName) return;
                _lastName = value;
                OnPropertyChanged();
            }
        }

        public string Gender
        {
            get { return _gender; }
            set
            {
                if (value == _gender) return;
                _gender = value;
                OnPropertyChanged();
            }
        }

        public DateTime Birthday
        {
            get { return _birthday; }
            set
            {
                if (value.Equals(_birthday)) return;
                _birthday = value;
                OnPropertyChanged();
            }
        }


        public NetworkedPerson(PeopleNetworkMessenger networkMessenger)
        {
            _networkMessenger = networkMessenger;
        }

        public void ChangeMultipleValues(IPersonUpdater updater)
        {
            updater.UpdateValues(this);
            _networkMessenger.Update(new PersonDto
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                Gender = Gender,
                Birthday = Birthday
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}