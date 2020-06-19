using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;

namespace MMSuperMarket
{
    public static class ProductHandler
    {
        public static void AddProductToInventory()
        {
            Console.WriteLine("Enter the following information about the product");
            ProductType pt = GetProductTypeByID();
            if (pt == null)
            {
                Console.WriteLine("Product type not found");
                return;
            }
            Console.WriteLine("How much new products to add? (Numbers Only): ");
            string string_amount = Console.ReadLine();
            int amount = 0;
            try { amount = int.Parse(string_amount); }
            catch { Console.WriteLine("Wrong Input"); return; }
            Inventory.Instance.AddToInventory(pt.ID, amount);
            Console.WriteLine($"{pt.Name} stock updated by {amount}. Current stock is {Inventory.Instance.GetInventoryInSuper()[pt]}");
        }

        public static ProductType GetProductTypeByID()
        {
            string ID = ValidatedID();
            foreach (var pt in Inventory.Instance.GetInventoryInSuper().Keys)
            {
                if (pt.ID == ID)
                    return pt;
            }
            return null;
        }

        public static ProductType GetProductTypeByID(string ID)
            // used only for new products created
        {
            foreach (var pt in Inventory.Instance.GetProductTypes())
            {
                if (pt.ID == ID)
                    return pt;
            }
            return null;
        }

        public static ProductType ProductToProductType(Product p)
        {
            foreach (var pt in Inventory.Instance.GetInventoryInSuper().Keys)
            {
                if (pt.ID == p.ID)
                    return pt;
            }
            return null;
        }


        

        public static ProductOrder GetOrderByID()
        {
            string id = ValidatedID();
            foreach (var pt in Inventory.Instance.GetOrderDB())
            {
                if (pt.OrderID == id)
                    return pt;
            }
            return null;
        }

        public static string ValidatedID()
        {
            int cID = 0;
            bool validUserInput = false;
            while (validUserInput == false)
            {
                try
                {
                    Console.Write("Enter product/order ID (Numbers Only): ");
                    cID = int.Parse(Console.ReadLine());
                    validUserInput = true;
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("Wrong input!"); Console.WriteLine();
                }
            }
            return cID.ToString();
        }

        public static void PrintProductTypes()
        {
            var table = new ConsoleTable("Product Type", "ID");
            foreach(var pt in Inventory.Instance.GetProductTypes())
            {
                table.AddRow(pt.Name, pt.ID);
            }
            table.Write();
        }
    }
}
