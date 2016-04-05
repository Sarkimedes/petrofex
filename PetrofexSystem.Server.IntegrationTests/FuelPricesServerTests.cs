using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetrofexSystem.PricesServer.Client.FuelSupplyService;
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
            var fuelPriceWcfClient = new FuelSupplyServiceClient();
            var wcfQuote = fuelPriceWcfClient.GetFuelPrices(1).QuotePrices;
            

            var fuelPrices = fuelPricesServer.GetFuelPrices();
            
            Assert.IsTrue(fuelPrices.ContainsKey(FuelType.Diesel));
            Assert.IsTrue(fuelPrices.ContainsKey(FuelType.Hydrogen));
            Assert.IsTrue(fuelPrices.ContainsKey(FuelType.LPG));
            Assert.IsTrue(fuelPrices.ContainsKey(FuelType.Unleaded));
            Assert.AreEqual(fuelPrices[FuelType.Diesel], wcfQuote.Where(x => x.Name == FuelType.Diesel.ToString()).FirstOrDefault().Price);
            
        }
    }
}
