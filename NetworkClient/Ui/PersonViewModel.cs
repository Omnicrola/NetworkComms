using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NetworkClient.Annotations;
using NetworkClient.Data;

namespace NetworkClient.Ui
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        public IPerson Person { get; set; }

        public string FirstName => Person.FirstName;
        public string LastName => Person.LastName;
        public string Gender => Person.Gender;
        public DateTime Birthday => Person.Birthday;

        public PersonViewModel(IPerson person)
        {
            Person = person;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}