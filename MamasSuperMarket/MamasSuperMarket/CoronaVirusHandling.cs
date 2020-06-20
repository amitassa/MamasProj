using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSuperMarket
{
    public class CoronaVirusHandling
    {
        public static bool TimeBetween(DateTime? na_datetime, DateTime? na_startDate, DateTime? na_endDate)
        {
            DateTime startDate = na_startDate.Value;
            TimeSpan start = startDate.TimeOfDay;
            DateTime endDate = na_endDate.Value;
            TimeSpan end = endDate.TimeOfDay;
            DateTime datetime = na_datetime.Value;
            TimeSpan now = datetime.TimeOfDay;

            // see if start comes before end
            if (start < end)
                return start <= now && now <= end;
            // start is after end, so do the inverse comparison
            return !(end < now && now < start);
        }

        public static void CustomerDiscoveredSick()
        {
            Console.WriteLine(); Console.WriteLine(); Console.WriteLine(); Console.WriteLine(); // Console view
            Console.WriteLine("Warning! did you end your employees shifts?");
            Console.WriteLine("Shift records only made after the employee ends his shift.");
            Console.WriteLine("If there is no shift history, there will no corona alerts!");
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
            Console.WriteLine(); Console.WriteLine(); Console.WriteLine(); Console.WriteLine(); // Console view
            string cust_id = PersonHandler.ValidatedID();
            Customer c = Program.GetCustomerByID(cust_id);
            if (c is null)
            {
                Console.WriteLine("Customer not found");
                return;
            }
            CashRegister CoronaCashRegister = null; // will contain the cash register the sick customer cheched out at
            DateTime? CoronaTime = null; // The time of the visit
            Employee IsolatedCashier = null;
            IEnumerable<CashRegister> AllRegs_Temp = new List<CashRegister>(SupermarketDB.Instance.GetCashRegisters());
            foreach (CashRegister cr in AllRegs_Temp)
            // finds the cash register the sick customer checked out at, and the time of the checkout
            {
                if (cr.CustomerHistory.ContainsKey(c))
                {
                    CoronaTime = cr.CustomerHistory[c];
                    CoronaCashRegister = cr;
                }
            }
            
            if (CoronaCashRegister is null)
                // if the client did not checkout at any cash register, no on should get into isolation
            {
                Console.WriteLine("At this point, there are no people who need to go into isolation.");
                return;
            }
            if (CoronaCashRegister.CashierHistory is null)
            // if there is no cashier history, we cannot check if the sick customer met anyone
            {
                Console.WriteLine("There is no cashier history! We cannot check whether employees/customers should go into isolation. ");
            }
            try
            {
                foreach (RegisterLog EmpUse in CoronaCashRegister.CashierHistory)
                {
                    if (TimeBetween(CoronaTime, EmpUse.StartTime, EmpUse.EndTime))
                    {
                        IsolatedCashier = EmpUse.UsingEmployee;
                        Console.WriteLine($"{EmpUse.UsingEmployee.FullName}, you need to get into isolation right away!");
                    }

                }
            }
            catch
            {
                Console.WriteLine("Client hasn`t checked out at any cash register");
            }
            bool IsIsolated = false;
            foreach (CashRegister cr in AllRegs_Temp)
            // for every use of the isolated cashier, the method checks which customers revealed to the cashier, and prints an isolation warning
            {
                foreach (RegisterLog EmpUse in cr.CashierHistory)
                {
                    
                    if (EmpUse.UsingEmployee == IsolatedCashier)
                    {
                        foreach (var CustVisit in cr.CustomerHistory)
                        {
                            if (TimeBetween((DateTime?)CustVisit.Value, EmpUse.StartTime, EmpUse.EndTime))
                            {
                                IsIsolated = true;
                                Console.WriteLine($"{CustVisit.Key.FullName}, you need to get into isolation right away!");
                            }
                        }
                    }
                }
            }
            if (IsIsolated == false)
                Console.WriteLine("At this point, there are no people who need to go into isolation.");

        }

    }
}
