using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSuperMarket
{
    public class Program
    {
        static void Main(string[] args)
        {
            // all of the following (Cash registers, Products and Customers) are for debugging

            SuperMarketQueue CustQueue = new SuperMarketQueue();
            Dictionary <ProductType, int> pts = Inventory.Instance.GetInventoryInSuper();
            CreateObjectsForDebug(ref CustQueue);
            MainMenu(ref CustQueue);
           
            Console.Read();
        }

        public static void MainMenu(ref SuperMarketQueue CustQueue)
        {
            // System client options menu
            StringBuilder sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine("Welcome to MamaSuperMarket Management System. Where would you like to access to?");
            sb.AppendLine();
            sb.AppendLine("1 - Line management");
            sb.AppendLine("2 - Employees management");
            sb.AppendLine("3 - Cash Register Management");
            sb.AppendLine("4 - Inventory Management");
            sb.AppendLine("5 - Report a Coronavirus sick past customer");
            sb.AppendLine("6 - Leave system");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(sb.ToString());
            Console.ForegroundColor = ConsoleColor.White;

            int choice = 8; // default value for the choice (if client`s input is wrong, program will get into the loop and give him another try)
            Console.Write("Your choice: ");
            string string_choice = Console.ReadLine(); // input for client`s choice
            try { choice = int.Parse(string_choice); } // trying to parse client`s choice to int
            catch { } // if client`s input is wrong, keeps choice as 5
            Console.Clear();

            while (choice != 7)
            {
                switch (choice)
                {
                    case 1:
                        LineManagement(ref CustQueue);
                        break;
                    case 2:
                        EmployeesManagement();
                        break;
                    case 3:
                        CashRegistersManamgement();
                        break;
                    case 4:
                        InventoryManagement();
                        break;
                    case 5:
                        CoronaVirusHandling.CustomerDiscoveredSick();
                        break;
                    case 6:
                        return;
                    default:
                        Console.WriteLine("Choice doesn`t exist, please try again.");
                        break;
                }
                sb = new StringBuilder();
                sb.AppendLine("Welcome to MamaSuperMarket Management System. Where would you like to access to?");
                sb.AppendLine();
                sb.AppendLine("1 - Line management");
                sb.AppendLine("2 - Employees management");
                sb.AppendLine("3 - Cash Register Management");
                sb.AppendLine("4 - Inventory Management");
                sb.AppendLine("5 - Report a Coronavirus sick past customer");
                sb.AppendLine("6 - Leave system");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(sb.ToString());
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Your choice: ");
                string_choice = Console.ReadLine();
                try { choice = int.Parse(string_choice); }
                catch { choice = 8; }
                Console.Clear();
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Press any key to continue. Bye bye.");
            Console.ForegroundColor = ConsoleColor.White;
        }
    
        public static void LineManagement(ref SuperMarketQueue CustQueue)
        /// <summary>
        /// A method to manage MamaSuperMarket queue.
        /// </summary>
        /// <param name="CustQueue">
        /// A reference to the supermarket queue in the program
        /// </param>
        {
            // System client options menu
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(); sb.AppendLine();
            sb.AppendLine("Welcome to MamaSuperMarket Line. What would you like to do?");
            sb.AppendLine();
            sb.AppendLine("1 - Append a customer to the line");
            sb.AppendLine("2 - Insert multiple customers to the supermarket");
            sb.AppendLine("3 - Watch who in the line");
            sb.AppendLine("4 - Go back to main menu");

            Console.WriteLine(sb.ToString());

            int choice = 5; // default value for the choice (if client`s input is wrong, program will get into the loop and give him another try)
            Console.Write("Your choice: ");
            string string_choice = Console.ReadLine(); // input for client`s choice
            try { choice = int.Parse(string_choice); } // trying to parse client`s choice to int
            catch { } // if client`s input is wrong, keeps choice as 5

            while (choice != 4)
            {
                switch (choice)
                {
                    case 1:
                        AppendCustomerToLine(ref CustQueue);
                        break;
                    case 2:
                        Console.Write("Customers to insert: ");
                        string string_count = Console.ReadLine();
                        int count = int.Parse(string_count);
                        CustQueue.InsertMultipleCustomersToSuperMarket(count);
                        break;
                    case 3:
                        CustQueue.CustomersInTheLine();
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("Choice doesn`t exist, please try again.");
                        break;
                }
                sb = new StringBuilder();
                sb.AppendLine(); sb.AppendLine();
                sb.AppendLine("Welcome to MamaSuperMarket Line. What would you like to do?");
                sb.AppendLine();
                sb.AppendLine("1 - Append a customer to the line");
                sb.AppendLine("2 - Insert multiple customers to the supermarket");
                sb.AppendLine("3 - Watch who in the line");
                sb.AppendLine("4 - Go back to main menu");
                Console.WriteLine(sb.ToString());
                Console.Write("Your choice: ");
                string_choice = Console.ReadLine();
                try { choice = int.Parse(string_choice); }
                catch { choice = 5; }
                Console.Clear();
            }

            Console.WriteLine("Press any key to continue. Bye bye.");
        }

        public static void AppendCustomerToLine(ref SuperMarketQueue SMQ)
        /// function that accepts a reference of the super market line, and append a customer to the line if it meets the requirements.
        /// if the customer join the line, a random shopping list is generated for him
        /// <input>
        /// by ValidatedID, ValidatedBodyHeat, IsMaskOn and ShouldBeIsolated methods on PersonHandler Class
        /// </input>
        /// <output>
        /// outputs by the GetInLine outputs in SuperMarketQueue class
        /// </output>
        {
            // start getting the information about the customer - ID, name, body heat, mask on, isolated
            Console.WriteLine("Please provide the following information");
            Console.Write("Enter customer`s full name: ");
            string FullName = Console.ReadLine();
            string cust_ID = PersonHandler.ValidatedID();
            double bh = PersonHandler.ValidateBodyHeat();
            bool mask = PersonHandler.IsMaskOn();
            bool isolate = PersonHandler.ShouldBeIsolated();
            Customer c = new Customer(FullName, cust_ID, bh, mask, isolate);
            SMQ.GetInLine(c);
            if (mask && isolate == false && PersonHandler.IsSick(bh) == false)
                GenerateRandomShoppingList(c);
            
        }

        public static void CashRegistersManamgement()
        {
            Console.Write("Welcome to cash register management menu. Please enter cash register number [1/2/3/4]: ");
            string reg_choice = Console.ReadLine();
            CashRegister CRegister = GetCashRegisterByNumber(reg_choice);
            if (CRegister == null)
            {
                Console.WriteLine("Cash register doesn`t exist.");
                return;
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(); sb.AppendLine();
            sb.AppendLine("What would you like to do?");
            sb.AppendLine();
            sb.AppendLine("1 - Products history");
            sb.AppendLine("2 - Cashiers history");
            sb.AppendLine("3 - Customers history");
            sb.AppendLine("4 - Go back to main menu");
            Console.WriteLine(sb.ToString());

            int choice = 5; // default value for the choice (if client`s input is wrong, program will get into the loop and give him another try)
            Console.Write("Your choice: ");
            string string_choice = Console.ReadLine(); // input for client`s choice
            try { choice = int.Parse(string_choice); } // trying to parse client`s choice to int
            catch { } // if client`s input is wrong, keeps choice as 5
            while (choice != 4)
            {
                switch (choice)
                {
                    case 1:
                        CRegister.ShowProductHistory();
                        break;
                    case 2:
                        CRegister.ShowCashierHistory();
                        break;
                    case 3:
                        CRegister.ShowCustomersHistory();
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("No such option");
                        break;
                }
                sb = new StringBuilder();
                sb.AppendLine(); sb.AppendLine();
                sb.AppendLine("What would you like to do?");
                sb.AppendLine();
                sb.AppendLine("1 - Products history");
                sb.AppendLine("2 - Cashiers history");
                sb.AppendLine("3 - Customers history");
                sb.AppendLine("4 - Go back to main menu");
                Console.WriteLine(sb.ToString());
                Console.Write("Your choice: ");
                string_choice = Console.ReadLine();
                try { choice = int.Parse(string_choice); }
                catch { choice = 5; }
                Console.Clear();
            }
        }

        public static void EmployeesManagement()
        {
            
            StringBuilder sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine("Welcome to employees management menu. What would you like to do?");
            sb.AppendLine("1 - Add new employee");
            sb.AppendLine("2 - Start / end a shift");
            sb.AppendLine("3 - Show employee`s debt");
            sb.AppendLine("4 - Show employee`s shift history");
            sb.AppendLine("5 - Go back to main menu");
            Console.WriteLine(sb.ToString());

            int choice = 6; // default value for the choice (if client`s input is wrong, program will get into the loop and give him another try)
            Console.Write("Your choice: ");
            string string_choice = Console.ReadLine(); // input for client`s choice
            try { choice = int.Parse(string_choice); } // trying to parse client`s choice to int
            catch { } // if client`s input is wrong, keeps choice as 5
            while (choice != 5)
            {
                switch (choice)
                {
                    case 1:
                        EmployeeHandler.AddNewEmp();
                        break;
                    case 2:
                        EmployeesShift();
                        break;
                    case 3:
                        EmployeeHandler.ShowEmpDebt();
                        break;
                    case 4:
                        EmployeeShiftsHistory();
                        break;
                    case 5:
                        break;
                    default:
                        break;
                }
                sb = new StringBuilder();
                sb.AppendLine();
                sb.AppendLine("Welcome to employees management menu. What would you like to do?");
                sb.AppendLine("1 - Add new employee");
                sb.AppendLine("2 - Start / end a shift");
                sb.AppendLine("3 - Show employee`s debt");
                sb.AppendLine("4 - Show employee`s shift history");
                sb.AppendLine("5 - Go back to main menu");
                Console.WriteLine(sb.ToString());

                choice = 6; // default value for the choice (if client`s input is wrong, program will get into the loop and give him another try)
                Console.Write("Your choice: ");
                string_choice = Console.ReadLine(); // input for client`s choice
                try { choice = int.Parse(string_choice); } // trying to parse client`s choice to int
                catch { } // if client`s input is wrong, keeps choice as 5
                Console.Clear();
            }
        }

        public static void InventoryManagement()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine("Welcome to inventory management menu. What would you like to do?");
            sb.AppendLine("1 - Add product to inventory");
            sb.AppendLine("2 - Show inventory & orders status");
            sb.AppendLine("3 - Update order status");
            sb.AppendLine("4 - Place an order");
            sb.AppendLine("5 - Show stock alerts");
            sb.AppendLine("6 - Go back to main menu");
            Console.WriteLine(sb.ToString());

            int choice = 7; // default value for the choice (if client`s input is wrong, program will get into the loop and give him another try)
            Console.Write("Your choice: ");
            string string_choice = Console.ReadLine(); // input for client`s choice
            try { choice = int.Parse(string_choice); } // trying to parse client`s choice to int
            catch { } // if client`s input is wrong, keeps choice as 5
            while (choice != 6)
            {
                switch (choice)
                {
                    case 1:
                        ProductHandler.AddProductToInventory();
                        break;
                    case 2:
                        InventoryHandler.ShowInventoryByStatus();
                        break;
                    case 3:
                        InventoryHandler.ChangeStatusToShipment();
                        break;
                    case 4:
                        InventoryHandler.PlaceOrder();
                        break;
                    case 5:
                        InventoryHandler.CheckInventoryStatus();
                        break;
                    default:
                        break;
                }
                sb = new StringBuilder();
                sb.AppendLine();
                sb.AppendLine("Welcome to inventory management menu. What would you like to do?");
                sb.AppendLine("1 - Add product to inventory");
                sb.AppendLine("2 - Show inventory & orders status");
                sb.AppendLine("3 - Update order status");
                sb.AppendLine("4 - Place an order");
                sb.AppendLine("5 - Show stock alerts");
                sb.AppendLine("6 - Go back to main menu");
                Console.WriteLine(sb.ToString());

                choice = 7 ; // default value for the choice (if client`s input is wrong, program will get into the loop and give him another try)
                Console.Write("Your choice: ");
                string_choice = Console.ReadLine(); // input for client`s choice
                try { choice = int.Parse(string_choice); } // trying to parse client`s choice to int
                catch { } // if client`s input is wrong, keeps choice as 5
                Console.Clear();
            }
        }

        public static void EmployeesShift()
            /// A method to start / end employees shifts.
            /// the method gets the ID as input from client, checks if the ID exists in the system
            /// the method let the client choose between start & end the shift by input, and does in accordance to that
        {
            string string_ID = PersonHandler.ValidatedID();
            Employee emp = EmployeeHandler.GetEmployeeByID(string_ID);
            if (emp == null) {return; }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"What would you like to do, {emp.FullName}?");
            sb.AppendLine("1 - Start a shift");
            sb.AppendLine("2 - End a shift");
            Console.WriteLine(sb.ToString());

            int choice = 3; // default value for the choice (if client`s input is wrong, program will get into the loop and give him another try)
            Console.Write("Your choice [1/2]: ");
            string string_choice = Console.ReadLine(); // input for client`s choice
            try { choice = int.Parse(string_choice); } // trying to parse client`s choice to int
            catch { } // if client`s input is wrong, keeps choice as 3

            switch (choice)
            {
                case 1:
                    EmployeeHandler.StartShift(emp);
                    break;
                case 2:
                    EmployeeHandler.EndShift(emp);
                    break;
                default:
                    Console.WriteLine("No such option");
                    EmployeesManagement();
                    break;
            }
           
        }

        public static void GenerateRandomShoppingList(Customer c)
            /// Generates random 3 products shopping list for the customer and prints it
        {
            List<ProductType> pTypes = Inventory.Instance.GetProductTypes();
            for (int i =0; i<3; i++)
            {
                Random Rand = new Random();
                int num = Rand.Next(1, 6);
                c.AddProductToShoppingList(pTypes[num]);
            }
            string ShopList = $"{c.FullName} shopping list is: ";
            foreach(ProductType p in c.ShoppingList)
            {
                ShopList += p.Name + ", ";
            }
            int charindex = ShopList.LastIndexOf(',');
            ShopList = ShopList.Remove(charindex, 2);
            Console.WriteLine(ShopList);
        }

        public static CashRegister GetCashRegisterByNumber(string Number)
        {
            foreach (CashRegister cr in SupermarketDB.Instance.GetCashRegisters())
            {
                if (cr.RegisterNumber == Number)
                    return cr;
            }
            return null;
        }

        public static Customer GetCustomerByID(string ID)
        {
            foreach (Customer c in SupermarketDB.Instance.GetCustomers())
            {
                if (c.ID == ID)
                    return c;
            }
            return null;
        }

        public static void EmployeeShiftsHistory()
        {
            string string_ID = PersonHandler.ValidatedID();
            Employee emp = EmployeeHandler.GetEmployeeByID(string_ID);
            if (emp == null) { return; }
            EmployeeHandler.ShowEmployeeShifts(emp);
        }

        /// <summary>
        /// The method creates 3 Employees, 6 Customers, 4 Cash Registers, 6 Product types and many products.
        /// </summary>
        public static void CreateObjectsForDebug(ref SuperMarketQueue CustQueue)
        {

            CashRegister CRegister1 = new CashRegister("1");
            CashRegister CRegister2 = new CashRegister("2");
            CashRegister CRegister3 = new CashRegister("3");
            CashRegister CRegister4 = new CashRegister("4");

            Console.WriteLine("Cash reg created");
            

            ProductType Yogo = new ProductType("Yogurt", 6.99, "111");
            Inventory.Instance.AddProductType(Yogo);
            ProductType Koteg = new ProductType("Koteg", 7.99, "222");
            Inventory.Instance.AddProductType(Koteg);
            ProductType Antrikot = new ProductType("Antrikot", 129.99, "333");
            Inventory.Instance.AddProductType(Antrikot);
            ProductType Tea = new ProductType("Tea Box", 25.00, "444");
            Inventory.Instance.AddProductType(Tea);
            ProductType Tomatoes = new ProductType("Tomatoes", 3.99, "555");
            Inventory.Instance.AddProductType(Tomatoes);
            ProductType Zero = new ProductType("Coca Cola Zero", 2.99, "666");
            Inventory.Instance.AddProductType(Zero);

            Console.WriteLine("Product Types Created");
            

            Customer c1 = new Customer("Amit Assa", "11", 36.5, true, false);
            CustQueue.GetInLine(c1);
            GenerateRandomShoppingList(c1);
            Customer c2 = new Customer("Dana Assa", "12", 34.5, true, false);
            CustQueue.GetInLine(c2);
            GenerateRandomShoppingList(c2);
            Customer c3 = new Customer("Dor Assa", "13", 37.2, false, true);
            CustQueue.GetInLine(c3);
            GenerateRandomShoppingList(c3);
            Customer c4 = new Customer("Chen Assa", "14", 38.3, true, false);
            CustQueue.GetInLine(c4);
            Customer c5 = new Customer("Mike Assa", "15", 36.0, true, true);
            CustQueue.GetInLine(c5);
            GenerateRandomShoppingList(c5);
            Customer c6 = new Customer("Shay Assa", "16", 32.1, true, false);
            CustQueue.GetInLine(c6);
            GenerateRandomShoppingList(c6);

            Console.WriteLine("Customers Created");
            

            Employee emp = new Employee("Yossi", "999");
            Employee emp2 = new Employee("Tanya", "998");
            Employee emp3 = new Employee("Lily", "997");

            Console.WriteLine("Employees created");
            

            CreateProductsDebug();
        }

        /// Creates many products (hardcoded, change manually )
        public static void CreateProductsDebug()
        {
            Random rand = new Random();
            Product product;
            for (int i =0; i <6; i++)
            {
                int ToExpDays = rand.Next(1, 16);
                product = new Product("Yogurt", 6.99, "111", ToExpDays);
                
            }
            for (int i = 0; i < 2; i++)
            {
                int ToExpDays = rand.Next(1, 16);
                product = new Product("Koteg", 7.99, "222", ToExpDays);
                
            }
            for (int i = 0; i < 23; i++)
            {
                int ToExpDays = rand.Next(1, 16);
                product = new Product("Antrikot", 129.99, "333", ToExpDays);
                
            }
            for (int i = 0; i < 21; i++)
            {
                int ToExpDays = rand.Next(1, 16);
                product = new Product("Tea Box", 25.00, "444", ToExpDays);
                
            }
            for (int i = 0; i < 21; i++)
            {
                int ToExpDays = rand.Next(1, 16);
                product = new Product("Tomatoes", 3.99, "555", ToExpDays);
                
            }
            for (int i = 0; i < 9; i++)
            {
                int ToExpDays = rand.Next(1, 16);
                product = new Product("Coca Cola Zero", 2.99, "666", ToExpDays);
                
            }

            Console.WriteLine("Products Created");
        }
    }
}
