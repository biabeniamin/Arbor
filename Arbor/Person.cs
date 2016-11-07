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
        private int _mother;
        private int _father;

        public int Father
        {
            get { return _father; }
            set { _father = value; }
        }

        public int Mother
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
        public Person(int id,string name,string description):this(id,name,description,-1,-1)
        {

        }
        public Person(int id, string name, string description,int fatherId, int motherId)
        {
            _id = id;
            _name = name;
            _description = description;
            _father = fatherId;
            _mother = motherId;
        }
        public override string ToString()
        {
            return _name;
        }
    }
}
