using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone.Classes
{
    public class UserInterface
    {
        private VendingMachine vendingMachine = new VendingMachine();

        public void RunInterface()
        {
            if(vendingMachine.ReadError != "")
            {
                Console.WriteLine(vendingMachine.ReadError);
                Console.WriteLine("Please exit the vending machine and call Customer Support");
                Console.WriteLine();
            }

            bool done = false;
            while (!done)
            {
                // Top Level Menu
                Console.Clear();
                PrintHeader();
                Console.WriteLine("Main Menu");
                Console.WriteLine("==========================================");
                Console.WriteLine("(1) Display Vending Machine Items");
                Console.WriteLine("(2) Purchase");
                Console.WriteLine("(3) End");

                string mainInput = Console.ReadLine();
                Console.WriteLine();

                switch (mainInput)
                {
                    case "1":
                        Display(); // Display list of all items with slot, name, and price
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey();
                        break;
                    case "2":
                        Purchase(); // Progress to purchasing menu
                        break;
                    case "3":
                        done = true; // Exit vending machine
                        break;
                    case "9":
                        vendingMachine.SalesReport(); // Hidden selection, run sales report

                        if (vendingMachine.WriteError != "")
                        {
                            Console.WriteLine(vendingMachine.WriteError);
                            Console.WriteLine("Please exit the vending machine and call Customer Support");
                            Console.WriteLine();
                        }

                        break;
                    default:
                        Console.WriteLine("Please enter a valid selection.");
                        Console.WriteLine();
                        break;
                }

                void Display() // Display list of all snack items
                {
                    Console.Clear();
                    PrintHeader();
                    Console.WriteLine(" Slot | Snack Item          | Price | Qty ");
                    Console.WriteLine("==========================================");
                    foreach (VendingMachineItem item in vendingMachine.items) 
                    {
                        Console.Write($"  {item.Slot}   ");
                        Console.Write($" {item.Name}".PadRight(22));
                        Console.Write($" ${item.Price}".PadRight(7));
                        if (item.Stock == 0)
                        {
                            Console.WriteLine("  OUT OF STOCK");
                        }
                        else
                        {
                            Console.WriteLine($"   {item.Stock}");
                        }
                    }
                    Console.WriteLine("==========================================");
                    Console.WriteLine();

                    return;
                }

                void Purchase() // Purchase an item from the machine
                {
                    bool purchase = true;
                    while (purchase)
                    {
                        Console.Clear();
                        PrintHeader();
                        Console.WriteLine("Purchase Menu");
                        Console.WriteLine("==========================================");
                        Console.WriteLine("(1) Feed Money");
                        Console.WriteLine("(2) Select Product");
                        Console.WriteLine("(3) Finish Transaction");

                        Console.WriteLine();
                        Console.WriteLine($"Current Balance: ${vendingMachine.Balance}");


                        string purchaseInput = Console.ReadLine();
                        Console.WriteLine();


                        switch (purchaseInput)
                        {
                            case "1": // Ya pays your money and ya takes yer chances
                                bool feeding = true;
                                while (feeding)
                                {
                                    Console.Clear();
                                    PrintHeader();
                                    Console.WriteLine("How much would you like to add?");
                                    Console.WriteLine("1) $1 ");
                                    Console.WriteLine("2) $2 ");
                                    Console.WriteLine("3) $5 ");
                                    Console.WriteLine("4) $10 ");
                                    Console.WriteLine("5) Return to Purchase Menu");
                                    Console.WriteLine();
                                    Console.WriteLine($"Current Balance: ${vendingMachine.Balance}");

                                    string addedMoney = Console.ReadLine();
                                    Console.WriteLine();

                                    switch (addedMoney) // Add various whole-dollar denominations
                                    {
                                        case "1":
                                            vendingMachine.AddMoney(1);
                                            break;
                                        case "2":
                                            vendingMachine.AddMoney(2);
                                            break;
                                        case "3":
                                            vendingMachine.AddMoney(5);
                                            break;
                                        case "4":
                                            vendingMachine.AddMoney(10);
                                            break;
                                        case "5": // Exit Feed Money menu, return to Purchase menu
                                            feeding = false;
                                            break;
                                        default:
                                            Console.WriteLine("Please enter a valid amount");
                                            Console.WriteLine();
                                            break;
                                    }

                                    if (vendingMachine.WriteError != "")
                                    {
                                        Console.WriteLine(vendingMachine.WriteError);
                                        Console.WriteLine("Please exit the vending machine and call Customer Support");
                                        Console.WriteLine();
                                    }
                                }

                                break;
                            case "2":
                                Display(); // Select snack item
                                Console.WriteLine("Please enter a valid slot number to purchase the item");
                                string slot = Console.ReadLine().ToUpper();

                                Console.WriteLine(vendingMachine.Purchase(slot));
                                Console.WriteLine();
                                Console.WriteLine("Press any key to continue.");
                                Console.ReadKey();

                                if (vendingMachine.WriteError != "")
                                {
                                    Console.WriteLine(vendingMachine.WriteError);
                                    Console.WriteLine("Please exit the vending machine and call Customer Support");
                                    Console.WriteLine();
                                }

                                break;
                            case "3": // Exit transaction, receive change, return to top menu
                                Console.WriteLine(vendingMachine.MakeChange());
                                Console.WriteLine();
                                Console.WriteLine("Press any key to continue.");
                                Console.ReadKey();

                                if (vendingMachine.WriteError != "")
                                {
                                    Console.WriteLine(vendingMachine.WriteError);
                                    Console.WriteLine("Please exit the vending machine and call Customer Support");
                                    Console.WriteLine();
                                }

                                purchase = false;
                                break;

                            default:
                                Console.WriteLine("Please enter a valid selection.");
                                Console.WriteLine();
                                break;
                        }
                    }
                }
            }
        }

        public void PrintHeader()
        {
            Console.WriteLine();
            Console.WriteLine(@"   _____                  __         ____      ______                    ____  ____  ____  ____   ");
            Console.WriteLine(@"  / ___/____  ____ ______/ /__      / __ \    /_  __/________  ____     / __ \/ __ \/ __ \/ __ \  ");
            Console.WriteLine(@"  \__ \/ __ \/ __ `/ ___/ //_/_____/ / / /_____/ / / ___/ __ \/ __ \   / /_/ / / / / / / / / / /  ");
            Console.WriteLine(@" ___/ / / / / /_/ / /__/ ,< /_____/ /_/ /_____/ / / /  / /_/ / / / /   \__, / /_/ / /_/ / /_/ /   ");
            Console.WriteLine(@"/____/_/ /_/\__,_/\___/_/|_|      \____/     /_/ /_/   \____/_/ /_/   /____/\____/\____/\____/    ");
            Console.WriteLine();
        }
    }
}

