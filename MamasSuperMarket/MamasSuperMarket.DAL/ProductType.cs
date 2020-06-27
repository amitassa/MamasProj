using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamasSuperMarket.DAL
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
        public static ProductType GetProductTypeByID(string defID = "")
        {
            string ID = "";
            if (String.IsNullOrWhiteSpace(defID))
                ID = ValidatedID();
            else
                ID = defID;
            foreach (var pt in Inventory.Instance.GetInventoryInSuper().Keys)
            {
                if (pt.ID == ID)
                    return pt;
            }
            return null;
        }

        ///<summary>
        /// The method gets an input from the console for ID, and validates it.
        /// The method will loop until the input is validated (Numbers Only)
        /// </summary>
        public static string ValidatedID()
        {
            int cID = 0;
            bool validUserInput = false;
            while (validUserInput == false)
            {
                try
                {
                    Console.Write("Enter product`s (Numbers Only): ");
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

    }
}
