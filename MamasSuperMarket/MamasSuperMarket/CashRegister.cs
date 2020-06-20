using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;

namespace MMSuperMarket
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

        public void CustomerCheckout(Customer c)
            /// <summary>
            /// Customer starts the checkout at the cash register.
            /// The checkout is recorded at this.CustomerHistory, and add the customer to the register line.
            /// </summary>
        {
            Console.WriteLine($"Hey {c.FullName}, Welcome to cash register {this.RegisterNumber}. What would you like to buy?");
            this.CustomerHistory.Add(c, DateTime.Now);
            foreach (ProductType pt in c.ShoppingList)
                BuyAProduct(pt);
            this.CustomersQueue.Add(c);

        }

        private void BuyAProduct(ProductType pr)
        /// <summary>
        /// A method to buy a product. The buying action only record the product in product history for this cash register
        /// </summary>
        {

            if (InventoryHandler.BuyAProduct(pr))
            {
                Console.WriteLine($"Product {pr.Name} bought for {pr.Price}.");
                this.ProductsBought.Add(pr);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{pr.Name} out of stock!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            InventoryHandler.CheckInventoryStatus();
        }

        public void StartTheCashRegister(Employee emp)
            /// <summary>
            /// A method to start the cash register.
            /// The starting action changes this.IsStarted to true and updates this.CurrentUse
            /// If the cash register is already started, prints an error
            /// </summary>
        {
            if (this.IsStarted == true)
            {
                Console.WriteLine("Erorr! Someone already using this cash register.");
            }
            else
            {
                this.IsStarted = true;
                this.CurrentUse = new RegisterLog(DateTime.Now, emp);
                Console.WriteLine($"Cashier {emp.FullName} is working on cash register {this.RegisterNumber}.");
            }
        }

        /// <summary>
        /// A method to start the cash register.
        /// The starting action changes this.IsStarted to false, record the use in this.CashierHistory and reset currentUse.
        /// </summary>
        public void StopTheCashRegister(Employee emp)
        {
            this.IsStarted = false;
            this.CurrentUse.EndTime = DateTime.Now;
            this.CashierHistory.Add(this.CurrentUse);
            this.CurrentUse = new RegisterLog();
            Console.WriteLine($"Cashier {emp.FullName} stopped working on cash register {this.RegisterNumber}.");
        }

        /// <summary>
        /// for every cashier used the cash register, prints his history 
        /// </summary>
        public void ShowCashierHistory()
        {
            var table = new ConsoleTable("Cashier Name", "Start Time", "End Time");
            foreach (RegisterLog record in this.CashierHistory)
            {
                table.AddRow(record.UsingEmployee.FullName, record.StartTime.ToString(), record.EndTime.ToString());
            }
            table.Write();
        }

        public void ShowProductHistory()
        {
            var table = new ConsoleTable("Product Name", "Price");
            foreach (ProductType pr in this.ProductsBought)
            {
                table.AddRow(pr.Name, pr.Price.ToString());
            }
            table.Write();
        }

        public void ShowCustomersHistory()
        {
            var table = new ConsoleTable("Customer`s Name", "Customer`s ID", "Visit Time");
            foreach (var dict in this.CustomerHistory)
            {
                table.AddRow(dict.Key.FullName, dict.Key.ID, dict.Value);
            }
            table.Write();
        }

        /// <summary>
        /// The method checks what cash register is the most available (the least customers in line) and returns it.
        /// </summary>
        public static CashRegister MostAvailableCashRegister()
        {
            CashRegister cr = null;
            if (SupermarketDB.Instance.GetCashRegisters().Count == 0)
            {
                return null ;
            }
            if (OpenedCashRegisters().Count == 0)
            {
                return null;
            }
            else
            {
                int MinimunLine = SupermarketDB.Instance.GetCashRegisters()[0].CustomersQueue.Count;
                cr = SupermarketDB.Instance.GetCashRegisters()[0];
                foreach (var cashReg in SupermarketDB.Instance.GetCashRegisters())
                {
                    if (cashReg.CustomersQueue.Count < MinimunLine && cashReg.IsStarted == true)
                    {
                        MinimunLine = cashReg.CustomersQueue.Count;
                        cr = cashReg;
                    }
                }
            }
           
            return cr;
        }

        public static CashRegister GetClosedCashRegister()
            /// function returns the next not used cash register
        {
            foreach (CashRegister cr in SupermarketDB.Instance.GetCashRegisters())
            {
                if (cr.IsStarted == false)
                    return cr;
            }
            return null;
        }

        public static List<CashRegister> OpenedCashRegisters()
            // returns a list of all opened cash registers
        {
            //string available = "The open cash registers are: ";
            List<CashRegister> OpenedCashReg = new List<CashRegister>();
            foreach (var cashReg in SupermarketDB.Instance.GetCashRegisters())
            {
                if (cashReg.IsStarted == true)
                {
                    //available += cashReg.RegisterNumber + ", ";
                    OpenedCashReg.Add(cashReg);
                }
            }
            //Console.WriteLine(available);
            return OpenedCashReg;
        }
    }
}
