using System.Windows;
using NetworkClient.Data;

namespace NetworkClient.Ui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(DataSource<IPerson> peopleDataSource)
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(peopleDataSource);
        }
    }
}