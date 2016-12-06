using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NetworkClient.Data;

namespace NetworkClient.Ui
{
    /// <summary>
    /// Interaction logic for PeopleTable.xaml
    /// </summary>
    public partial class PeopleTable : UserControl
    {
        public static readonly DependencyProperty PeopleProperty = DependencyProperty.Register("People",
            typeof(List<IPerson>), typeof(PeopleTable), new PropertyMetadata(default(List<IPerson>)));

        public static readonly DependencyProperty SelectedPersonProperty = DependencyProperty.Register(
            "SelectedPerson", typeof(PersonViewModel), typeof(PeopleTable),
            new PropertyMetadata(default(PersonViewModel)));

        public PeopleTable()
        {
            InitializeComponent();
            RootElement.DataContext = this;
        }

        public List<IPerson> People
        {
            get { return (List<IPerson>)GetValue(PeopleProperty); }
            set { SetValue(PeopleProperty, value); }
        }

        public PersonViewModel SelectedPerson
        {
            get { return (PersonViewModel)GetValue(SelectedPersonProperty); }
            set { SetValue(SelectedPersonProperty, value); }
        }
    }
}