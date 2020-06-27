using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;
using MamasSuperMarket.DAL;

namespace MamasSuperMarket.BLL
{
    public static class EmployeeHandler
    {
        public static void AddNewEmp()
        /// A method to add new employee to the system
        {

            Console.Clear();
            Console.WriteLine("Please provide the following information:");
            string ID = PersonHandler.ValidatedID();

            // check if ID already exists in the system
            try
            {
                foreach (Employee emp in SupermarketDB.Instance.GetEmployees())
                {
                    if (emp.ID == ID)
                    {
                        Console.WriteLine("ID is already used");
                        return;
                    }
                }
            }
            catch{ }
            Console.Write("Enter employee`s name: ");
            string emp_name = Console.ReadLine();
            Employee New_Emp = new Employee(emp_name, ID);
            Console.WriteLine($"New employee created! Good luck, {New_Emp.FullName}");

        }

        public static void StartShift(Employee emp)
        /// <summary>
        /// A method to start the epmloyee`s shift (starts the moment he calls this function - like swiping a card)
        /// start a shift means this.CurrentShift is updating
        /// The program picks the next available cash register which the employee will work on, and starts it
        /// if the last shift wasn`t ended (forgot to check out for example), he could not check in
        /// </summary>
        {

            if (emp.CurrentShift.Count == 1)
            {
                Console.WriteLine("Error! Your last shift was`nt ended. Please check out and try again");
            }
            else
            {
                if (IsEmployeeAllowedToWork(emp))
                {
                    CashRegister CurrentCR = CashRegisterBL.GetClosedCashRegister();
                    if (CurrentCR is null)
                    {
                        Console.WriteLine($"No cash register available. You can`t start your shift, {emp.FullName}");
                        return;
                    }
                    emp.CurrentShift.Add(DateTime.Now);
                    emp.CurrentCashRegister = CurrentCR.RegisterNumber;
                    CashRegisterBL.StartTheCashRegister(emp, CurrentCR);
                    Console.WriteLine($"Welcome, {emp.FullName}. Your cash register is {CurrentCR.RegisterNumber}");
                    //CashRegister.OpenedCashRegisters(); // for debugging
                }
                else
                {
                    Console.WriteLine("You cannot work like that. Go home! Your debt has grown by 40 ILS.");
                    emp.Debt += 40;
                }

            }
        }

        public static void EndShift(Employee emp)
        /// <summary>
        /// A method to end the employee`s shift. 
        /// First checking if this.CurrentShift has been started. 
        /// If it does, the method ends the current shift, record it in this.Shifts, resets the current shift, ends the cash register and resets the current cash register property
        /// If not, prints an error
        /// </summary>
        {
            if (emp.CurrentShift.Count == 1 && emp.CurrentCashRegister != "0")
            {
                emp.CurrentShift.Add(DateTime.Now);
                Console.WriteLine($"Goodbye, {emp.FullName}");
                SubFromDebtByWork(emp.CurrentShift[0], emp.CurrentShift[1], emp);
                emp.Shifts.Add(emp.CurrentShift);
                emp.CurrentShift = new List<DateTime>();
                CashRegister CurrentCR = CashRegisterBL.GetCashRegisterByNumber(emp.CurrentCashRegister);
                CashRegisterBL.StopTheCashRegister(emp, CurrentCR);

            }
            else
            {
                Console.WriteLine("Error! There is no active shift. Please check in first.");
            }
        }

        public static bool IsEmployeeAllowedToWork(Employee emp)
        {
            double bh = PersonHandler.ValidateBodyHeat();
            bool mask = PersonHandler.IsMaskOn();
            bool isolate = PersonHandler.ShouldBeIsolated();

            return (mask && isolate == false && PersonHandler.IsSick(bh) == false);
        }

        public static void ShowEmpDebt()
        {
            ShowEmployees();
            string string_ID = PersonHandler.ValidatedID();
            Employee emp = GetEmployeeByID(string_ID);
            if (emp is null)
            {
                return;
            }
            double WorkHoursDebt = emp.Debt / 30.0;
            Console.WriteLine($"{emp.FullName}, your debt is {emp.Debt}, which are {WorkHoursDebt} hours of work.");
        }

        public static void ShowEmployeeShifts(Employee emp)
        {
            var table = new ConsoleTable("Employee`s Name", "Start Time", "End Time");
            foreach (var shift in emp.Shifts)
            {
                table.AddRow(emp.FullName, shift[0].ToString(), shift[1].ToString());
            }
            table.Write();
        }

        public static void SubFromDebtByWork(DateTime startTime, DateTime endTime ,Employee emp)
        {
            // if the is no debt, there is nothing to substract from
            if (emp.Debt == 0)
            {
                return;
            }

            // calculate shift time by hours
            double hours = (endTime - startTime).TotalHours;
            double shiftWage = hours * 30; // 30ILS per hour

            if (shiftWage > emp.Debt)
                emp.Debt = 0; // debt cannot be negative
            else
                emp.Debt -= shiftWage;

            Console.WriteLine($"{emp.FullName}, your debt is down by {shiftWage}ILS, to {emp.Debt}ILS");
        }

        public static Employee GetEmployeeByID(string ID)
        /// Gets an ID an return the Employee object that ID belongs to. If there is no match, return null.
        {
            IEnumerable<Employee> Emps = SupermarketDB.Instance.GetEmployees();
            foreach (Employee emp in Emps)
            {
                if (emp.ID == ID)
                {
                    return emp;
                }
            }
            Console.WriteLine("Employee not found");
            return null;
        }

        public static void EmployeeShiftsHistory()
        {
            ShowEmployees();
            string string_ID = PersonHandler.ValidatedID();
            Employee emp = EmployeeHandler.GetEmployeeByID(string_ID);
            if (emp is null) { return; }
            ShowEmployeeShifts(emp);
        }

        public static void ShowEmployees()
        {
            var table = new ConsoleTable("Employee`s Name", "Employee`s ID", "Is on active shift");
            foreach (var emp in SupermarketDB.Instance.GetEmployees())
            {
                bool IsOnShift = emp.CurrentShift.Count != 0;
                string shift;
                if (IsOnShift)
                    shift = "True";
                else
                    shift = "False";

                table.AddRow(emp.FullName, emp.ID, shift);
            }
            Console.WriteLine(table);
        }
    }
}
