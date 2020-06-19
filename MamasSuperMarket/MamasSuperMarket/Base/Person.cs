using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSuperMarket
{
    public abstract class Person
    {
        public string FullName { get; }
        public string ID { get; }
        public Person(string name, string ID)
        {
            this.FullName = name;
            this.ID = ID;
        }


    }
}
