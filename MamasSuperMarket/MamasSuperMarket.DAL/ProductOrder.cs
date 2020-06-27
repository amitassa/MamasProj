using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamasSuperMarket.DAL
{
    public class ProductOrder
    {
        public ProductType productType { get; }
        public int OrderAmount { get; }
        public string OrderID { get; }
        public string OrderStatus { get; set; } // possible values - Factory / Shipment
        public ProductOrder(ProductType pt, int Amount, string ID)
        {
            productType = pt;
            OrderAmount = Amount;
            OrderID = ID;
            OrderStatus = "Factory";
        }


        
    }
}
