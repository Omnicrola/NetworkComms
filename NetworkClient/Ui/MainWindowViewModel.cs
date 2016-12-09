using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using Dragablz;
using NetworkClient.Data;

namespace NetworkClient.Ui
{
    public class MainWindowViewModel
    {
        public ObservableCollection<PersonViewModel> People { get; set; }
        public PersonViewModel CurrentlySelectedPerson { get; set; }
        public SimpleInterTabClient InterTabClient { get; set; }

        public MainWindowViewModel(DataSource<IPerson> peopleDataSource)
        {
            People = new ObservableCollection<PersonViewModel>();
            peopleDataSource.Data.DataChanged += HandlePeopleChanged;
            InterTabClient = new SimpleInterTabClient();
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

    public class SimpleInterTabClient : IInterTabClient
    {
        public INewTabHost<Window> GetNewHost(IInterTabClient interTabClient, object partition, TabablzControl source)
        {
            var tabWindow = new TabWindow();
            return new NewTabHost<Window>(tabWindow, tabWindow.TabablzControl);
        }

        public TabEmptiedResponse TabEmptiedHandler(TabablzControl tabControl, Window window)
        {
            throw new NotImplementedException();
        }
    }
}