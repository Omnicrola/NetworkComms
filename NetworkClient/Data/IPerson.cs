using System;
using System.ComponentModel;

namespace NetworkClient.Data
{
    public interface IPerson : INotifyPropertyChanged
    {
        int Id { get; }
        string FirstName { get; }
        string LastName { get; }
        string Gender { get; }
        DateTime Birthday { get; }

        void ChangeMultipleValues(IPersonUpdater updater);
    }

    public interface IPersonUpdater
    {
        void UpdateValues(IPersonWriteable writeablePerson);
    }
}