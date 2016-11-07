using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arbor
{
    public class DatabaseOperations
    {
        private OleDbConnection _connection;
        private OleDbCommand _command;
        private OleDbDataReader _reader;
        public DatabaseOperations()
        {
            _connection = new OleDbConnection("Provider=Microsoft.Ace.Oledb.12.0;Data Source=db.accdb");
            try
            {
                _connection.Open();
            }
            catch(Exception ee)
            {
                System.Windows.MessageBox.Show("Conexiunea cu baza de date nu poate fi initializata!");
            }
        }
        public OleDbDataReader GetReader(string query)
        {
            _command = new OleDbCommand(query, _connection);
            _reader = _command.ExecuteReader();
            return _reader;
        }
        public void ExecuteQuery(string query)
        {
            _command = new OleDbCommand(query,_connection);
            _command.ExecuteNonQuery();
        }
        public type ReadOneValue<type>(string query)
        {
            GetReader(query);
            type result = default(type);
            _reader.Read();
            result = (type)_reader[0];
            return result;
        }
        public Person GetPerson(int id)
        {
            Person person = default(Person);
            GetReader($"select * from People where Id={id}");
            if (_reader.Read())
            {
                string name = _reader[1].ToString();
                int gen = (int)_reader[2];
                string description = _reader[3].ToString();
                int fatherId = (int)_reader[4];
                int motherId = (int)_reader[5];
                person = new Person(id, name, description, fatherId, motherId);
            }
            return person;
        }
        public ObservableCollection<Person> GetAllPersons()
        {
            ObservableCollection<Person> persons = new ObservableCollection<Person>();
            GetReader($"select * from People");
            while(_reader.Read())
            {
                int id = (int)_reader[0];
                string name = _reader[1].ToString();
                int gen = (int)_reader[2];
                string description = _reader[3].ToString();
                int fatherId = (int)_reader[4];
                int motherId = (int)_reader[5];
                Person person = new Person(id, name, description, fatherId, motherId);
                persons.Add(person);
            }
            return persons;
        }
        public Person AddPerson(string name,int gen, string description, int fatherId, int motherId)
        {
            string query = $"insert into people(nume,gen,descriere,fatherId,motherId) values('{name}',{gen},'{description}',{fatherId},{motherId})";
            ExecuteQuery(query);
            int id = ReadOneValue<int>($"select id from people where nume='{name}' and fatherId={fatherId} and motherId={motherId}");
            return GetPerson(id);
        }
    }
}
