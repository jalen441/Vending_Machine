using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class VendingMachineItem
    {
        //properties = reference input, like the c4 thing, name, price (do we store amount left here?
        //probably not, that seems to be part of the vending machine
        public string Slot { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Message { get; set; }

        public VendingMachineItem()
        {
            Stock = 5;
        }
    }
}
