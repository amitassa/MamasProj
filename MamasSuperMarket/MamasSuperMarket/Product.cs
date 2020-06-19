using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSuperMarket
{
    public class Product : ProductType
    {
        public int DaysToExpire;
        public Product(string productName, double price, string ID, int daysToExpire) : base(productName, price, ID)
        {
            DaysToExpire = daysToExpire;
            Inventory.Instance.AddProductToList(this);
            Inventory.Instance.AddToInventory(ID, 1);
        }
    }
}
