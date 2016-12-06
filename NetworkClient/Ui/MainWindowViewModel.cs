using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using NetworkClient.Data;

namespace NetworkClient.Ui
{
    public class MainWindowViewModel
    {
        public ObservableCollection<PersonViewModel> People { get; set; }
        public PersonViewModel SelectedPerson { get; set; }

        public MainWindowViewModel()
        {
            People = new ObservableCollection<PersonViewModel>();
            MasterDataSource.Instance().PeopleDataSource.Data.DataChanged += HandlePeopleChanged;
        }

        private void HandlePeopleChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (IPerson personAdded in e.NewItems)
                {
                    People.Add(new PersonViewModel(personAdded));
                }
            }

            if (e.OldItems != null)
            {
                foreach (IPerson personRemoved in e.OldItems)
                {
                    var vmFound = People.FirstOrDefault(p => p.Person.Id == personRemoved.Id);
                    if (vmFound != null)
                    {
                        People.Remove(vmFound);
                    }
                }
            }
        }
    }
}