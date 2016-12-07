using System.Windows;
using System.Windows.Controls;
using NetworkClient.Data;

namespace NetworkClient.Ui
{
    public partial class SelectedUserPanel : UserControl
    {
        public static readonly DependencyProperty SelectedPersonProperty = DependencyProperty.Register(
            "SelectedPerson", typeof(PersonViewModel), typeof(SelectedUserPanel),
            new PropertyMetadata(default(PersonViewModel)));

        public SelectedUserPanel()
        {
            InitializeComponent();
            RootElement.DataContext = this;
        }

        public PersonViewModel SelectedPerson
        {
            get { return (PersonViewModel)GetValue(SelectedPersonProperty); }
            set { SetValue(SelectedPersonProperty, value); }
        }
    }
}