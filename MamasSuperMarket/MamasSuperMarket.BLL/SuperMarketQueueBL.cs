using System;
using System.Collections.Generic;
using System.Text;
using MamasSuperMarket.DAL;

namespace MamasSuperMarket.BLL
{
    public class SuperMarketQueueBL
    {
        public static void AppendCustomerToLine()
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
            GetInLine(c);
            if (mask && isolate == false && PersonHandler.IsSick(bh) == false)
                CustomerHandler.GenerateRandomShoppingList(c);

        }

        public static void NextInLine()
        {
            if (SuperMarketQueue.Instance.GetCustQueue().Count == 0)
            {
                //throw new ArgumentNullException(paramName: "CustQueue", message: "Customers queue is empty. There is no one in line");
                Console.WriteLine("Line is empty");
                Console.WriteLine(); // to organize console for a better understanding
            }
            else
            {

                CashRegister cr = CashRegisterBL.MostAvailableCashRegister();
                if (cr is null)
                {
                    Console.WriteLine("No cash register available");
                    return;
                }
                Customer c = SuperMarketQueue.Instance.RemoveFromQueue();
                CashRegisterBL.CustomerCheckout(c, cr);
                Console.WriteLine($"You can get in, {c.FullName}");
            }

        }

        public static void InsertMultipleCustomersToSuperMarket(int CustCount)
        {
            if (SuperMarketQueue.Instance.GetCustQueue().Count < CustCount)
                CustCount = SuperMarketQueue.Instance.GetCustQueue().Count;
            for (int i = 0; i < CustCount; i++)
            {
                NextInLine();
            }
            Console.WriteLine(); // to organize console for a better understanding
        }

        /// <summary>
        /// The method checks if the customer meet with the requirements - wearing a mask, shouldn`t be isolated and bodyheat under 38.0
        /// If the customer meets with the requirements, it gets in the line.
        /// If not, prints a message.
        /// </summary>
        public static void GetInLine(Customer c)

        {
            if (c.WearingMask == true && c.ShouldBeIsolated == false && PersonHandler.IsSick(c.BodyHeat) == false)
            {

                Console.WriteLine($"{c.FullName}. Your place in line is {SuperMarketQueue.Instance.GetCustQueue().Count}");
                Console.WriteLine(); // to organize console for a better understanding
                SuperMarketQueue.Instance.AddToQueue(c);
            }
            else
            {
                Console.WriteLine($"Sorry {c.FullName}, you do not meet with the requirements. you can`t get in line.");
                Console.WriteLine(); // to organize console for a better understanding
            }

        }


        public static void CustomersInTheLine()
        {
            Queue<Customer> NewCustQueue = new Queue<Customer>(SuperMarketQueue.Instance.GetCustQueue());
            string CustomersNames = null;
            foreach (Customer c in NewCustQueue)
            {
                CustomersNames += c.FullName + ", ";
            }

            if (!String.IsNullOrWhiteSpace(CustomersNames))
            {
                int charindex = CustomersNames.LastIndexOf(',');
                CustomersNames = CustomersNames.Remove(charindex, 2);
                Console.WriteLine($"There are {SuperMarketQueue.Instance.GetCustQueue().Count} customers in line:");
                Console.WriteLine(CustomersNames);
                Console.WriteLine(); // to organize console for a better understanding
            }
            else
            {
                Console.WriteLine("Line is empty");
                Console.WriteLine(); // to organize console for a better understanding
            }
        }
    }
}
