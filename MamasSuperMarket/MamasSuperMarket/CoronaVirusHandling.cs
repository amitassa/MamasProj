using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSuperMarket
{
    public class CoronaVirusHandling
    {
        public static bool TimeBetween(Nullable<DateTime> na_datetime, Nullable<DateTime> na_startDate, Nullable<DateTime> na_endDate)
        {
            DateTime startDate = (DateTime)na_startDate;
            TimeSpan start = startDate.TimeOfDay;
            DateTime endDate = (DateTime)na_endDate;
            TimeSpan end = endDate.TimeOfDay;
            DateTime datetime = (DateTime)na_datetime;
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
            Console.Write("Please enter customer`s ID: ");
            string cust_id = Console.ReadLine();
            Customer c = Program.GetCustomerByID(cust_id);
            if (c == null)
            {
                Console.WriteLine("Customer not found");
                return;
            }
            CashRegister CoronaCashRegister = null; // will contain the cash register the sick customer cheched out at
            Nullable<DateTime> CoronaTime = null; // The time of the visit
            Employee IsolatedCashier = null;
            List<CashRegister> AllRegs_Temp = new List<CashRegister>(SupermarketDB.Instance.GetCashRegisters());
            foreach (CashRegister cr in AllRegs_Temp)
            // finds the cash register the sick customer checked out at, and the time of the checkout
            {
                if (cr.CustomerHistory.ContainsKey(c))
                {
                    CoronaTime = cr.CustomerHistory[c];
                    CoronaCashRegister = cr;
                }
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
            catch (NullReferenceException ex)
            {
                Console.WriteLine("Client hasn`t checked out at any cash register");
            }
            foreach (CashRegister cr in AllRegs_Temp)
            // for every use of the isolated cashier, the method checks which customers revealed to the cashier, and prints an isolation warning
            {
                foreach (RegisterLog EmpUse in cr.CashierHistory)
                {
                    if (EmpUse.UsingEmployee == IsolatedCashier)
                    {
                        foreach (var CustVisit in cr.CustomerHistory)
                        {
                            if (TimeBetween((Nullable<DateTime>)CustVisit.Value, EmpUse.StartTime, EmpUse.EndTime))
                            {
                                Console.WriteLine($"{CustVisit.Key.FullName}, you need to get into isolation right away!");
                            }
                        }
                    }
                }
            }

        }

    }
}
