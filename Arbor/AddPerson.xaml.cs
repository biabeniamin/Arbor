using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace Arbor
{
    /// <summary>
    /// Interaction logic for AddPerson.xaml
    /// </summary>
    public partial class AddPerson : Page,INotifyPropertyChanged
    {
        private DatabaseOperations _dbOper;
        private ObservableCollection<Person> _persons;
        private Person _selectedFather;
        private Person _selectedMother;
        private string _nume;
        private string _description;
        private ObservableCollection<Gen> _gene;
        private DelegateCommand _addCommand;

        public DelegateCommand AddCommand
        {
            get { return _addCommand; }
            set { _addCommand = value; }
        }
        private Gen _selectedGen;

        public Gen SelectedGen
        {
            get { return _selectedGen; }
            set
            {
                _selectedGen = value;
                OnPropertyChanged("SelectedGen");
            }
        }

        public ObservableCollection<Gen> Gene
        {
            get
            {
                if (_gene == null)
                {
                    _gene = new ObservableCollection<Gen>();
                    var gene = Enum.GetValues(typeof(Gen));
                    foreach (var gen in gene)
                    {
                        _gene.Add((Gen)gen);
                    }
                }
                return _gene;
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        public string Nume
        {
            get { return _nume; }
            set
            {
                _nume = value;
                OnPropertyChanged("Nume");
            }
        }

        public Person SelectedMother
        {
            get { return _selectedMother; }
            set
            {
                _selectedMother = value;
                OnPropertyChanged("SelectedMother");
            }
        }

        public Person SelectedFather
        {
            get { return _selectedFather; }
            set
            {
                _selectedFather = value;
                OnPropertyChanged("SelectedFather");
            }
        }

        public ObservableCollection<Person> Persons
        {
            get { return _persons; }
            set { _persons = value; }
        }

        public AddPerson(DatabaseOperations dbOper)
        {
            InitializeComponent();
            _dbOper = dbOper;
            _persons = _dbOper.GetAllPersons();
            _addCommand = new DelegateCommand(AddPersonAction);
            _selectedGen = Gene[0];
            if(_persons.Count>0)
            {
                _selectedMother = _persons[0];
                _selectedFather = _persons[0];
            }
            DataContext = this;
        }
        private void AddPersonAction()
        {
            if (string.IsNullOrEmpty(Nume))
            {
                MessageBox.Show("Adauga nume!");
                return;
            }
            Person person = _dbOper.AddPerson(Name, (int)SelectedGen, Description, SelectedFather.Id, SelectedMother.Id);
            Persons.Add(person);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
