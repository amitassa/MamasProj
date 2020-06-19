using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSuperMarket
{
    public class ProductType
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string ID { get; }
        public ProductType(string productName, double price, string id)
        {
            Name = productName;
            Price = price;
            ID = id;
        }

        
    }
}
