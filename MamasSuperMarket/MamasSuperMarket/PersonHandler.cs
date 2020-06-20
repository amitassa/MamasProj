using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSuperMarket
{
    /// <summary>
    ///  The class purpose to handle with body heat, ID, name, mask on and isolation validations
    /// </summary>
    public class PersonHandler
    {
        ///<summary>
        /// The method gets an input from the console for bodyheat, and validates it.
        /// The method will loop until the input is validated (decimal number)
        /// </summary>
        public static double ValidateBodyHeat()
        {
            
            double bh = 0;
            bool validUserInput = false;
            while (validUserInput == false)
            {
                Console.Write("Enter person`s body heat (decimal number): ");
                try
                {
                    bh = double.Parse(Console.ReadLine()); // now bh = employee`s body heat
                    validUserInput = true;
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("Wrong input!"); Console.WriteLine();
                }
            }
            return bh;
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
                    Console.Write("Enter employee`s / customer`s ID (Numbers Only): ");
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

        /// <summary>
        /// The method checks if the person wears a mask on. True if he is, false otherwise
        /// The method first receive input from console, check its validation, and return true/false according to the input
        /// The method loops until the input is "y" / "n" 
        /// </summary>
        public static bool IsMaskOn()
        {

            bool validUserInput = false;
            bool mask = false;
            while (validUserInput == false)
            {
                Console.Write("Are you with a mask [y/n]? ");
                string mask_choice = Console.ReadLine();
                switch (mask_choice.ToLower())
                {
                    case "y":
                        mask = true;
                        validUserInput = true;
                        break;
                    case "n":
                        mask = false;
                        validUserInput = true;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Wrong input");
                        break;
                }
            }
            return mask;
        }

        /// <summary>
        /// The method checks if the person should be in isolation. True if he is, false otherwise
        /// The method first receive input from console, check its validation, and return true/false according to the input
        /// The method loops until the input is "y" / "n" 
        /// </summary>
        public static bool ShouldBeIsolated()
        {
            bool validUserInput = false;
            bool isolated = false;
            string mask_choice = null;
            while (validUserInput == false)
            {
                Console.Write("Do you need to be in isolation [y/n]? ");
                mask_choice = Console.ReadLine();
                switch (mask_choice.ToLower())
                {
                    case "y":
                        isolated = true;
                        validUserInput = true;
                        break;
                    case "n":
                        isolated = false;
                        validUserInput = true;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Wrong input");
                        break;
                }
            }
            return isolated;
        }

        /// <summary>
        /// function gets a body heat and chekcs if it meets Ministry of Health`s requirements.
        /// MOH requirements for body heat is lower than 38.0
        /// </summary>
        /// <param name="heat">
        /// positive number from double type
        /// </param>
        /// <returns>
        /// True if body heat meets the requirements. False otherwise.
        /// </returns>
        public static Boolean IsSick(double bodyTemparture) => bodyTemparture >= 38.0;
    }
}

