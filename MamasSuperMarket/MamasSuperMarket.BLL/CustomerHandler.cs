using System;
using System.Collections.Generic;
using System.Text;
using MamasSuperMarket.DAL;

namespace MamasSuperMarket.BLL

{
    class CustomerHandler
    {
        public static Customer GetCustomerByID(string ID)
        {
            foreach (Customer c in SupermarketDB.Instance.GetCustomers())
            {
                if (c.ID == ID)
                    return c;
            }
            return null;
        }

        public static void GenerateRandomShoppingList(Customer c)
        /// Generates random 3 products shopping list for the customer and prints it
        {
            List<ProductType> pTypes = Inventory.Instance.GetProductTypes();
            for (int i = 0; i < 3; i++)
            {
                Random Rand = new Random();
                int num = Rand.Next(1, 6);
                c.AddProductToShoppingList(pTypes[num]);
            }
            string ShopList = $"{c.FullName} shopping list is: ";
            foreach (ProductType p in c.ShoppingList)
            {
                ShopList += p.Name + ", ";
            }
            int charindex = ShopList.LastIndexOf(',');
            ShopList = ShopList.Remove(charindex, 2);
            Console.WriteLine(ShopList);
        }
    }
}
