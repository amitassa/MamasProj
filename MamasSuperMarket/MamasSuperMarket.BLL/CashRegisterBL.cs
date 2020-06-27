using System;
using System.Collections.Generic;
using System.Text;
using ConsoleTables;
using MamasSuperMarket.DAL;

namespace MamasSuperMarket.BLL
{
    class CashRegisterBL
    {
        public static CashRegister GetCashRegisterByNumber(string ID)
        {
            IEnumerable<CashRegister> CashRegs = SupermarketDB.Instance.GetCashRegisters();
            foreach (var cr in CashRegs)
            {
                if (cr.RegisterNumber == ID)
                    return cr;
            }
            return null;
        }

        public static void CustomerCheckout(Customer c , CashRegister cr)
        /// <summary>
        /// Customer starts the checkout at the cash register.
        /// The checkout is recorded at this.CustomerHistory, and add the customer to the register line.
        /// </summary>
        {
            Console.WriteLine($"Hey {c.FullName}, Welcome to cash register {cr.RegisterNumber}. What would you like to buy?");
            cr.CustomerHistory.Add(c, DateTime.Now);
            foreach (ProductType pt in c.ShoppingList)
                BuyAProduct(pt, cr);
            cr.CustomersQueue.Add(c);

        }

        private static void BuyAProduct(ProductType pr, CashRegister cr)
        /// <summary>
        /// A method to buy a product. The buying action only record the product in product history for this cash register
        /// </summary>
        {

            if (InventoryHandler.BuyAProduct(pr))
            {
                Console.WriteLine($"Product {pr.Name} bought for {pr.Price}.");
                cr.ProductsBought.Add(pr);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{pr.Name} out of stock!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            InventoryHandler.CheckInventoryStatus();
        }

        public static void StartTheCashRegister(Employee emp, CashRegister cr)
        /// <summary>
        /// A method to start the cash register.
        /// The starting action changes this.IsStarted to true and updates this.CurrentUse
        /// If the cash register is already started, prints an error
        /// </summary>
        {
            if (cr.IsStarted == true)
            {
                Console.WriteLine("Erorr! Someone already using this cash register.");
            }
            else
            {
                cr.IsStarted = true;
                cr.CurrentUse = new RegisterLog(DateTime.Now, emp);
                Console.WriteLine($"Cashier {emp.FullName} is working on cash register {cr.RegisterNumber}.");
            }
        }

        /// <summary>
        /// A method to start the cash register.
        /// The starting action changes this.IsStarted to false, record the use in this.CashierHistory and reset currentUse.
        /// </summary>
        public static void StopTheCashRegister(Employee emp, CashRegister cr)
        {
            cr.IsStarted = false;
            cr.CurrentUse.EndTime = DateTime.Now;
            cr.CashierHistory.Add(cr.CurrentUse);
            cr.CurrentUse = new RegisterLog();
            Console.WriteLine($"Cashier {emp.FullName} stopped working on cash register {cr.RegisterNumber}.");
        }

        /// <summary>
        /// for every cashier used the cash register, prints his history 
        /// </summary>
        public static void ShowCashierHistory(CashRegister cr)
        {
            var table = new ConsoleTable("Cashier Name", "Start Time", "End Time");
            foreach (RegisterLog record in cr.CashierHistory)
            {
                table.AddRow(record.UsingEmployee.FullName, record.StartTime.ToString(), record.EndTime.ToString());
            }
            table.Write();
        }

        public static void ShowProductHistory(CashRegister cr)
        {
            var table = new ConsoleTable("Product Name", "Price");
            foreach (ProductType pr in cr.ProductsBought)
            {
                table.AddRow(pr.Name, pr.Price.ToString());
            }
            table.Write();
        }

        public static void ShowCustomersHistory(CashRegister cr)
        {
            var table = new ConsoleTable("Customer`s Name", "Customer`s ID", "Visit Time");
            foreach (var dict in cr.CustomerHistory)
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
                return null;
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

