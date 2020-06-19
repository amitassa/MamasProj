using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;

namespace MMSuperMarket
{
    public static class InventoryHandler
    {
        /// <summary>
        /// The method checks for every product type, 50% or more of its inventory are going to expire in a week, and prints an alert
        /// The method also checks for every product type if there are 20 or less pieces of the product in the inventory. If there are, prints an alert
        /// </summary>
        public static void CheckInventoryStatus()
        {
            int ExpiringAmount;
            foreach (var productInventory in Inventory.Instance.GetInventoryInSuper())
            {
                ExpiringAmount = 0;
                foreach (Product p in Inventory.Instance.GetProducts())
                {
                    if (p.DaysToExpire <= 7)
                        ExpiringAmount++;
                }
                if ((double)ExpiringAmount / (double)productInventory.Value >= 0.5)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Alert! 50% or more of the product {productInventory.Key.Name} is about to expire in a week! You should make an order!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                if(productInventory.Value <= 20)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Alert! There are only {productInventory.Value} pieces of {productInventory.Key.Name} in stock! You should make an order!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        /// <summary>
        /// The method prints a table of all inventory, in stock and orders, buy the status:
        /// In stock, Shipment, Accepted in factory
        /// </summary>
        public static void ShowInventoryByStatus()
        {
            List<ProductOrder> OrderDB = Inventory.Instance.GetOrderDB();
            Dictionary<ProductType, int> InventoryInSuper = Inventory.Instance.GetInventoryInSuper();
            var table = new ConsoleTable("Product Name", "Product ID", "Status", "Amount");
            foreach (var stock in InventoryInSuper)
            {
                table.AddRow(stock.Key.Name, stock.Key.ID, "In stock", stock.Value);
            }
            if (OrderDB.Count != 0)
            {
                foreach (var order in OrderDB)
                {
                    if (order.OrderStatus == "Shipment")
                        table.AddRow(order.productType.Name, order.productType.ID, "Shipment", order.OrderAmount);
                }
                foreach (var order in OrderDB)
                {
                    if (order.OrderStatus == "Factory")
                        table.AddRow(order.productType.Name, order.productType.ID, "Accepted in factory", order.OrderAmount);
                }
            }
            
            table.Write();

        }

        /// <summary>
        /// The method checks if the product is in stock when trying to buy it.
        /// The method converts the products to its product type, and calls SubstanceFromInventory method from Inventory singleton class.
        /// return true if in stock, flase otherwise.
        /// </summary>
        public static bool BuyAProduct(ProductType pt)
        {
            if (pt == null)
                return false;

            return Inventory.Instance.SubstanceFromInventory(pt);
        }

        public static void PlaceOrder()
        {
            ProductHandler.PrintProductTypes();
            Console.WriteLine("Fill the information below about the order:");
            string ID = ProductHandler.ValidatedID();
            int amount = 0;

            bool validUserInput = false;
            while (validUserInput == false)
            {
                try
                {
                    Console.Write("Enter the amount of products to order: ");
                    amount = int.Parse(Console.ReadLine()); // now bh = employee`s body heat
                    validUserInput = true;
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("Wrong input!"); Console.WriteLine();
                }
            }
            ProductType pt = ProductHandler.GetProductTypeByID(ID);
            if (pt == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Product not found");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            Random rand = new Random();
            int orderID = rand.Next(1, 999);
            Console.Clear();
            Console.WriteLine(); Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Order placed! Your order ID is: {orderID}");
            Console.ForegroundColor = ConsoleColor.White; 
            ProductOrder PO_temp = new ProductOrder(pt, amount, orderID.ToString());
            Inventory.Instance.AddOrderToDB(PO_temp);
        }

        public static void ChangeStatusToShipment()
        {
            ProductOrder PO = ProductHandler.GetOrderByID();
            if (PO == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Product order not found");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            PO.OrderStatus = "Shipment";
        }

    }
}
