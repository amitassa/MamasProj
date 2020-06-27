using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamasSuperMarket.DAL
{
    public class SuperMarketQueue
    {
        private static SuperMarketQueue instance = new SuperMarketQueue();
        static SuperMarketQueue()
        {

        }
        private SuperMarketQueue()
        {
            CustQueue = new Queue<Customer>();
        }
        public static SuperMarketQueue Instance
        {
            get
            {
                if (instance is null)
                {
                    instance = new SuperMarketQueue();
                }
                return instance;
            }
        }

        internal Queue<Customer> CustQueue;
       
        public Queue<Customer> GetCustQueue()
        {
            return CustQueue;
        }

        public void AddToQueue(Customer c)
        {
            CustQueue.Enqueue(c);
        }

        public Customer RemoveFromQueue()
        {
            return CustQueue.Dequeue();
        }
    }
}
