using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PumpLibrary;

namespace PetrofexSystem.Server.IntegrationTests
{
    [TestClass]
    public class FuelPricesServerTests
    {
        // Get fuel prices from WCF service
        [TestMethod]
        public void GetFuelPrices_ConnectsToWcfService_AndGetsFuelPrices()
        {
            var fuelPricesServer = new FuelPricesServer();
            
            var fuelPrices = fuelPricesServer.GetFuelPrices();
            
            Assert.IsTrue(fuelPrices.ContainsKey(FuelType.Diesel));
            Assert.IsTrue(fuelPrices.ContainsKey(FuelType.Hydrogen));
            Assert.IsTrue(fuelPrices.ContainsKey(FuelType.LPG));
            Assert.IsTrue(fuelPrices.ContainsKey(FuelType.Unleaded));
        }
    }
}
