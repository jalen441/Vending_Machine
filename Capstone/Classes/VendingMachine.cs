using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone.Classes
{
    public class VendingMachine
    {
        public List<VendingMachineItem> items = new List<VendingMachineItem>();
        private string filePath = @"..\..\..\..\etc\";  // Location if I/O files
        private string inFile = "vendingmachine.csv"; // Name of input file
        private string outFile = "log.txt"; // Name of log file
        private string salesReportName = $"SalesReport - {DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss")}(H.M.S).txt"; // Generate unique, date/time-stamped sales report file
        private decimal moneySpent;

        public decimal Balance { get; set; } // Amount of money in machine
        public string WriteError { get; set; }
        public string ReadError { get; set; }

        public VendingMachine()
        {
            Balance = 0.00M;
            ReadError = "";
            WriteError = "";

            try
            {
                using (StreamReader sr = new StreamReader(Path.Combine(filePath, inFile))) // Read input file and stock machine, i.e., populate list(items) with all snacks
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] snackData = line.Split('|');

                        if (snackData[0][0] == 'A')
                        {
                            Chips chips = new Chips(snackData);
                            items.Add(chips);
                        }
                        else if (snackData[0][0] == 'B')
                        {
                            Candy candy = new Candy(snackData);
                            items.Add(candy);
                        }
                        else if (snackData[0][0] == 'C')
                        {
                            Drinks drink = new Drinks(snackData);
                            items.Add(drink);
                        }
                        else if (snackData[0][0] == 'D')
                        {
                            Gum gum = new Gum(snackData);
                            items.Add(gum);
                        }
                    }
                }
            }
            catch (IOException e) // Catch file read error
            {
                ReadError = "Error stocking the vending machine.\n" + e.Message;
            }
        }

        public decimal AddMoney(decimal addedMoney) // Feed money into the machine
        {
            Balance += addedMoney; // Increase Balance by amount added
            try
            {
                using (StreamWriter sw = new StreamWriter(Path.Combine(filePath, outFile), true)) // Write transaction to audit log
                {
                    sw.Write($"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")} ");
                    sw.Write("FEED MONEY ".PadRight(24));
                    sw.Write($"${Balance - addedMoney}".PadRight(10));
                    sw.WriteLine($"${string.Format("{0:0.00}", Balance)}");
                }
            }
            catch (IOException e) // Catch write error
            {
                WriteError = "Error writing to the log file.\n" + e.Message;
            }

            return Balance;
        }

        public string Purchase(string slot) // Purchase item from machine
        {
            string result = "";
            foreach (VendingMachineItem item in items)
            {
                if (item.Slot == slot)
                {
                    if (item.Stock == 0) // Check if item is in stock
                    {
                        result = "Sorry, that item is sold out.";
                        break;
                    }

                    else if (Balance >= item.Price) // If Balance is high enough, purchase item
                    {
                        result = $"{item.Message}";
                        item.Stock--; // Decrease item Stock by 1
                        Balance = Balance - item.Price; // Decrease Balance by item price
                        moneySpent += item.Price;
                        try
                        {
                            using (StreamWriter sw = new StreamWriter(Path.Combine(filePath, outFile), true)) // Write transaction to audit log
                            {
                                sw.Write($"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")} ");
                                sw.Write($"{item.Name}".PadRight(20));
                                sw.Write($"{item.Slot}  ");
                                sw.Write($"${Balance + item.Price}".PadRight(10));
                                sw.WriteLine($"${string.Format("{0:0.00}", Balance)}");
                            }
                        }
                        catch (IOException e) // Catch write error
                        {
                            WriteError = "Error writing to the log file.\n" + e.Message;
                        }
                        break;
                    }
                    else if (Balance < item.Price) // Inform user if Balance is too low
                    {
                        result = "You don't have enough money!";
                        break;
                    }
                }
                else // Inform user if invalid item code is entered
                {
                    result = "Sorry, that item does not exist.";
                }
            }
            return result;
        }

        public string MakeChange() // Return change in coins when transaction is ended
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(Path.Combine(filePath, outFile), true)) // Write transaction to audit log
                {
                    sw.Write($"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")} ");
                    sw.Write($"GIVE CHANGE ".PadRight(24));
                    sw.Write($"${string.Format("{0:0.00}", Balance)}".PadRight(10));
                    sw.WriteLine($"$0.00");
                }
            }
            catch (IOException e) // Catch write error
            {
                WriteError = "Error writing to the log file.\n" + e.Message;
            }

            // Calculate change

            string result = "";
            int balance = (int)(Balance * 100);
            int quarters = (balance / 25);
            balance = balance - quarters * 25;
            int dimes = (balance / 10);
            balance = balance - dimes * 10;
            int nickles = (balance / 5);

            result = $"Your change is ${string.Format("{0:0.00}", Balance)}, {quarters} quarters, {dimes} dimes, and {nickles} nickles.";
            Balance = 0; // Reset Balance

            return result;
        }

        public void SalesReport() // Write sales report file with unique file name, including date and time
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(Path.Combine(filePath, salesReportName)))
                {
                    foreach (VendingMachineItem item in items)
                    {
                        sw.WriteLine($"{item.Name} | {5 - item.Stock}"); // Write each item with quantity sold
                    }
                    sw.WriteLine();
                    sw.WriteLine($" **TOTAL SALES**   ${moneySpent}"); // Write total sale amount
                }
            }
            catch (IOException e) // Catch write error
            {
                WriteError = "Error writing to the sales report file.\n" + e.Message;
            }
        }

    }
}
