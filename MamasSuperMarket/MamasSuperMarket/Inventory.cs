using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSuperMarket
{
    // Singleton
    public class Inventory
    {
        private static Inventory instance = new Inventory();
        static Inventory()
        {

        }
        private Inventory()
        {
            OrderDB = new List<ProductOrder>();
            InventoryInSuper = new Dictionary<ProductType, int>();
            Products = new List<Product>();
            ProductTypes = new List<ProductType>();
        }
        public static Inventory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Inventory();
                }
                return instance;
            }
        }

        internal List<ProductType> ProductTypes;
        internal List<ProductOrder> OrderDB;
        internal Dictionary<ProductType, int> InventoryInSuper;
        internal List<Product> Products;

        public List<ProductOrder> GetOrderDB()
        {
            return OrderDB;
        }

        public void AddOrderToDB(ProductOrder PO)
        {
            OrderDB.Add(PO);
        }

        public List<ProductType> GetProductTypes()
        {
            return ProductTypes;
        }

        public void AddProductType(ProductType PT)
        {
            if (!ProductTypes.Contains(PT))
            {
                ProductTypes.Add(PT);
                AddProducTypeToInventory(PT);
            }
            
        }

        public void ProductBought(ProductType pt)
        {
            foreach (Product product in Products)
            {
                if (product.ID == pt.ID)
                {
                    Products.Remove(product);
                    return;
                }
            }
            Console.WriteLine("Failed buying product");
        }

        public List<Product> GetProducts()
        {
            return Products;
        }

        public void AddProductToList(Product p)
        {
            Products.Add(p);
        }

        public Dictionary<ProductType, int> GetInventoryInSuper()
        {
            return InventoryInSuper;
        }

        public void AddToInventory(string ProductTypeID, int Amount)
        {
            ProductType pt = ProductHandler.GetProductTypeByID(ProductTypeID);
            try
            {
                InventoryInSuper[pt] += Amount;
            }
            catch
            {
                Console.WriteLine("Adding stock failed");
            }
        }

        public void AddProducTypeToInventory(ProductType pt)
        {
            if (!InventoryInSuper.ContainsKey(pt))
            {
                InventoryInSuper.Add(pt, 0);
            }
        }


        /// <summary>
        /// after every product bought, the method called to substance from inventory.
        /// if the stock of the product is greater than 0, will substance 1 from stock and return true
        /// if the stock of the product is 0, the method will return false and the buying try won`t success
        /// </summary>
        /// <param name="pt">The product type the customer tries to buy</param>
        public bool SubstanceFromInventory(ProductType pt)
        {
            if (InventoryInSuper[pt] == 0)
            {
                return false;
            }
            else
            {
                InventoryInSuper[pt] = InventoryInSuper[pt] - 1;
                return true;
            }
        }

    }
}
