using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSuperMarket
{
    public class SuperMarketQueue
    {
        public Queue<Customer> CustQueue{get; }
        public SuperMarketQueue()
        {
            this.CustQueue = new Queue<Customer>();
        }

        public void NextInLine()
        {
            if (this.CustQueue.Count == 0)
            {
                //throw new ArgumentNullException(paramName: "CustQueue", message: "Customers queue is empty. There is no one in line");
                Console.WriteLine("Line is empty");
                Console.WriteLine(); // to organize console for a better understanding
            }
            else
            {
                
                CashRegister cr = CashRegister.MostAvailableCashRegister();
                if (cr == null)
                {
                    Console.WriteLine("No cash register available");
                    return;
                }
                Customer c = this.CustQueue.Dequeue();
                cr.CustomerCheckout(c);
                Console.WriteLine($"You can get in, {c.FullName}");
            }
            
        }

        public void InsertMultipleCustomersToSuperMarket(int CustCount)
        {
            if (this.CustQueue.Count < CustCount)
                CustCount = this.CustQueue.Count;
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
        public void GetInLine(Customer c)

        {
            if (c.WearingMask == true && c.ShouldBeIsolated == false && PersonHandler.IsSick(c.BodyHeat) == false)
            {

                Console.WriteLine($"{c.FullName}. Your place in line is {this.CustQueue.Count}");
                Console.WriteLine(); // to organize console for a better understanding
                this.CustQueue.Enqueue(c);
            }
            else
            {
                Console.WriteLine($"Sorry {c.FullName}, you do not meet with the requirements. you can`t get in line.");
                Console.WriteLine(); // to organize console for a better understanding
            }
            
        }
        
        
        public void CustomersInTheLine()
        {
            Queue<Customer> NewCustQueue = new Queue<Customer>(this.CustQueue);
            string CustomersNames = "";
            foreach (Customer c in NewCustQueue)
            {
                CustomersNames += c.FullName + ", ";
            }

            if (CustomersNames != "")
            {
                int charindex = CustomersNames.LastIndexOf(',');
                CustomersNames = CustomersNames.Remove(charindex, 2);
                Console.WriteLine($"There are {this.CustQueue.Count} customers in line:");
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
