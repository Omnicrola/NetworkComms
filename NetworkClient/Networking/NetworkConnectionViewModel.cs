using System.ComponentModel;
using System.Runtime.CompilerServices;
using NetworkClient.Annotations;

namespace NetworkClient.Networking
{
    public class NetworkConnectionViewModel : INotifyPropertyChanged
    {
        private readonly NetworkConnectionModel _networkConnectionModel;
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

        public NetworkConnectionViewModel(NetworkConnectionModel networkConnectionModel)
        {
            _networkConnectionModel = networkConnectionModel;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}