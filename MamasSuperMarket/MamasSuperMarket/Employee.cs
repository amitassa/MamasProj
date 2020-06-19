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
        public List<List<DateTime>> Shifts { get; set; }
        public double Debt { get; set; }
        public List<DateTime> CurrentShift { get; set; }
        public string CurrentCashRegister { get; set; }
        public Employee(string fullname, string ID) : base(fullname, ID)
        {
            this.Shifts = new List<List<DateTime>>();
            this.CurrentShift = new List<DateTime>();
            this.Debt = 0;
            this.CurrentCashRegister = "0";
            SupermarketDB.Instance.AddEmployeeToList(this);

        }

    }
}
