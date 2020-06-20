using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSuperMarket
{
    public class Customer : Person
    {
        public double BodyHeat { get; set; } // property defined in the class in order to be able to create hardcoded objects, without input
        public DateTime VisitTimeStamp { get; }
        public List<ProductType> ShoppingList { get; } = new List<ProductType>();
        public bool WearingMask { get; } // property defined in the class in order to be able to create hardcoded objects, without input
        public bool ShouldBeIsolated { get; } // property defined in the class in order to be able to create hardcoded objects, without input
        public Customer(string name, string ID, double heat, bool Mask, bool isolate) : base(name, ID)
        {
            BodyHeat = heat;
            VisitTimeStamp = DateTime.Now;
            WearingMask = Mask;
            ShouldBeIsolated = isolate;
            SupermarketDB.Instance.AddCustomerToList(this);
        }

        ///<summary>
        /// Adding a product to customer`s shopping list
        /// </summary>
        public void AddProductToShoppingList(ProductType pt)
        {
            this.ShoppingList.Add(pt);
            Console.WriteLine($"{pt.Name} was added to your shopping list, {FullName}");
        }
        

    }
}
