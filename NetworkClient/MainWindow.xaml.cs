using System.Windows;
using NetworkClient.Networking;
using NetworkLibrary.DataTransferObjects;
using NetworkLibrary.NetworkMessages;

namespace NetworkClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly PeopleViewModel _peopleViewModel;

        public MainWindow()
        {
            InitializeComponent();
            _peopleViewModel = new PeopleViewModel();
            this.DataContext = _peopleViewModel;
        }

        private void OnClick_AddPerson(object sender, RoutedEventArgs e)
        {
            _peopleViewModel.AddPerson(new PersonDto { FirstName = FirstName.Text, LastName = LastName.Text });
        }
    }
}