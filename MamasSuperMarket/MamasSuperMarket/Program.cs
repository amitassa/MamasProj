﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MamasSuperMarket.BLL;

namespace MamasSuperMarket.APP
{
    public class Program
    {
        static void Main(string[] args)
        {
            Menu.CreateObjectsForDebug();
            Menu.MainMenu();
           
            Console.Read();
        }
    }
}
