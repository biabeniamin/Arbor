using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arbor
{
    public class Person
    {
        private int _id;
        private string _name;
        private string _description;
        private Person _mother;
        private Person _father;

        public Person MyProperty
        {
            get { return _father; }
            set { _father = value; }
        }

        public Person Mother
        {
            get { return _mother; }
            set { _mother = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public Person(int id,string name,string description):this(id,name,description,null,null)
        {

        }
        public Person(int id, string name, string description,Person father,Person mother)
        {
            _id = id;
            _name = name;
            _description = description;
            _father = father;
            _mother = mother;
        }
    }
}
