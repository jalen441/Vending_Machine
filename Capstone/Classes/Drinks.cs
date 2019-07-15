using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Drinks : VendingMachineItem
    {
        public Drinks(string[] snackData)
        {
            Slot = snackData[0];
            Name = snackData[1];
            Price = decimal.Parse(snackData[2]);
            Message = "Glug Glug, Yum!";
        }
    }
}
