using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone;
using Capstone.Classes;

namespace CapstoneTests
{
    [TestClass]
    public class VendingMachineTests
    {
        VendingMachine vendingMachine = new VendingMachine();

        [TestMethod]
        public void AddMoneyTest()
        {
            Assert.AreEqual(2, vendingMachine.AddMoney(2));
            Assert.AreEqual(7, vendingMachine.AddMoney(5));
        }

        [TestMethod]
        public void BalanceTest()
        {
            Assert.AreEqual(0, vendingMachine.Balance);
            vendingMachine.AddMoney(10);
            Assert.AreEqual(10, vendingMachine.Balance);
            vendingMachine.AddMoney(5);
            Assert.AreEqual(15, vendingMachine.Balance);
        }

        [TestMethod]
        public void PurchaseTest()
        {
            vendingMachine.Balance = 0;
            Assert.AreEqual("You don't have enough money!", vendingMachine.Purchase("A1"));

            vendingMachine.items[0].Stock = 0;
            Assert.AreEqual("Sorry, that item is sold out.", vendingMachine.Purchase("A1"));

            Assert.AreEqual("Sorry, that item does not exist.", vendingMachine.Purchase("Z9"));

            vendingMachine.Balance = 5.00M;
            Assert.AreEqual("Crunch Crunch, Yum!", vendingMachine.Purchase("A4"));
            Assert.AreEqual("Glug Glug, Yum!", vendingMachine.Purchase("C1"));
        }

        [TestMethod]
        public void ChangeTest()
        {
            vendingMachine.Balance = 11.65M;
            Assert.AreEqual("Your change is $11.65, 46 quarters, 1 dimes, and 1 nickles.", vendingMachine.MakeChange());

            vendingMachine.Balance = 1.30M;
            Assert.AreEqual("Your change is $1.30, 5 quarters, 0 dimes, and 1 nickles.", vendingMachine.MakeChange());

            vendingMachine.Balance = 10.45M;
            Assert.AreEqual("Your change is $10.45, 41 quarters, 2 dimes, and 0 nickles.", vendingMachine.MakeChange());

            vendingMachine.Balance = 0.15M;
            Assert.AreEqual("Your change is $0.15, 0 quarters, 1 dimes, and 1 nickles.", vendingMachine.MakeChange());
        }

    }
}
