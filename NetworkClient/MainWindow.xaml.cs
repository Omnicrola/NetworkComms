using System.Windows;
using NetworkClient.Networking;
using NetworkLibrary.NetworkMessages;

namespace NetworkClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NetworkConnectionViewModel _networkConnectionViewModel;
        private NetworkManager _networkManager;

        public MainWindow()
        {
            InitializeComponent();
            _networkManager = new NetworkManager();
            _networkConnectionViewModel = new NetworkConnectionViewModel(new NetworkConnectionModel());
            this.DataContext = _networkConnectionViewModel;
        }

        private void OnClick_SendMessage(object sender, RoutedEventArgs e)
        {
            this._networkConnectionViewModel.LastResponse =
                _networkManager.SendMessage(UserMessage.Text);
        }
    }
}