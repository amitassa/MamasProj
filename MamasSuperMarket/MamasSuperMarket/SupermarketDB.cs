using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSuperMarket
{
    // Singleton
    public class SupermarketDB
    {
        private static SupermarketDB instance = new SupermarketDB();
        static SupermarketDB()
        {

        }
        private SupermarketDB()
        {
            Customers = new List<Customer>();
            Employees = new List<Employee>();
            CashRegisters = new List<CashRegister>();
        }
        public static SupermarketDB Instance
        {
            get
            {
                if (instance is null)
                {
                    instance = new SupermarketDB();
                }
                return instance;
            }
        }

        internal List<CashRegister> CashRegisters;
        internal List<Customer> Customers;
        internal List<Employee> Employees;
        
        public List<Customer> GetCustomers()
        {
            return Customers;
        }

        public void AddCustomerToList(Customer c)
        {
            Customers.Add(c);
        }

        public List<Employee> GetEmployees()
        {
            return Employees;
        }

        public void AddEmployeeToList(Employee emp)
        {
            Employees.Add(emp);
        }

        public List<CashRegister> GetCashRegisters()
        {
            return CashRegisters;
        }
        
        public void AddCashRegisterToList(CashRegister cr)
        {
            CashRegisters.Add(cr);
        }


    }
}
