using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Chips : VendingMachineItem
    {
        public Chips(string[] snackData)
        {
            Slot = snackData[0];
            Name = snackData[1];
            Price = decimal.Parse(snackData[2]);
            Message = "Crunch Crunch, Yum!";
        }
    }
}
