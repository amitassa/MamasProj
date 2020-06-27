using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;

namespace MamasSuperMarket.DAL
{
    public class CashRegister
    {
        public List<ProductType> ProductsBought { get; } = new List<ProductType>(); // List of all products bought at this cash register
        public Dictionary<Customer, DateTime> CustomerHistory { get; } = new Dictionary<Customer, DateTime>(); // History of all customers checked out at this cash register
        public List<RegisterLog> CashierHistory { get; } = new List<RegisterLog>(); // History of all cashiers used this cash register
        public RegisterLog CurrentUse { get; set; } // The current use of a cashier at this cash register
        public List<Customer> CustomersQueue { get; } = new List<Customer>(); // The line for this cash register
        public bool IsStarted { get; set; } = false; // Is a cashier using this cash register now
        public string RegisterNumber { get; } // The number of this cash register

        public CashRegister(string registerNumber)
        {

            this.RegisterNumber = registerNumber;
            SupermarketDB.Instance.AddCashRegisterToList(this);
        }

    }       
}
