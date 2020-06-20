using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;

namespace MMSuperMarket
{
    public class Employee : Person
    {
        public List<List<DateTime>> Shifts { get; set; } = new List<List<DateTime>>();
        public double Debt { get; set; } = 0;
        public List<DateTime> CurrentShift { get; set; } = new List<DateTime>();
        public string CurrentCashRegister { get; set; } = "0";
        public Employee(string fullname, string ID) : base(fullname, ID)
        {
            SupermarketDB.Instance.AddEmployeeToList(this);
        }

    }
}
