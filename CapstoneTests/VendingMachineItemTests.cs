using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone;
using Capstone.Classes;

namespace CapstoneTests
{
    [TestClass]
    public class VendingMachineItemTests
    {
        VendingMachine vendingMachine = new VendingMachine();
        
        [TestMethod]
        public void ChipsPropertyCheck()
        {
            Assert.AreEqual("A1", vendingMachine.items[0].Slot);
            Assert.AreEqual("Potato Crisps", vendingMachine.items[0].Name);
            Assert.AreEqual(3.05M, vendingMachine.items[0].Price);
            Assert.AreEqual(5, vendingMachine.items[0].Stock);
            Assert.AreEqual("Crunch Crunch, Yum!", vendingMachine.items[0].Message);
        }

        [TestMethod]
        public void CandyPropertyCheck()
        {
            Assert.AreEqual("B1", vendingMachine.items[4].Slot);
            Assert.AreEqual("Moonpie", vendingMachine.items[4].Name);
            Assert.AreEqual(1.8M, vendingMachine.items[4].Price);
            Assert.AreEqual(5, vendingMachine.items[4].Stock);
            Assert.AreEqual("Munch Munch, Yum!", vendingMachine.items[4].Message);
        }

        [TestMethod]
        public void DrinksPropertyCheck()
        {
            Assert.AreEqual("C1", vendingMachine.items[8].Slot);
            Assert.AreEqual("Cola", vendingMachine.items[8].Name);
            Assert.AreEqual(1.25M, vendingMachine.items[8].Price);
            Assert.AreEqual(5, vendingMachine.items[8].Stock);
            Assert.AreEqual("Glug Glug, Yum!", vendingMachine.items[8].Message);
        }

        [TestMethod]
        public void GumPropertyCheck()
        {
            Assert.AreEqual("D1", vendingMachine.items[12].Slot);
            Assert.AreEqual("U-Chews", vendingMachine.items[12].Name);
            Assert.AreEqual(.85M, vendingMachine.items[12].Price);
            Assert.AreEqual(5, vendingMachine.items[12].Stock);
            Assert.AreEqual("Chew Chew, Yum!", vendingMachine.items[12].Message);
        }

        
    }
}
