using System;
using System.Collections.Generic;
using System.Configuration;
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
        public void GetFuelPrices_WithNoDataInCache_GetsFuelPriceData()
        {
            var fuelPricesServer = new FuelPricesServer(new PriceCache());
            var fuelPriceWcfClient = new FuelSupplyServiceClient();
            var wcfQuote = fuelPriceWcfClient.GetFuelPrices(1).QuotePrices;

            var fuelPrices = fuelPricesServer.GetFuelPrices();
            
            Assert.IsTrue(fuelPrices.ContainsKey(FuelType.Diesel));
            Assert.IsTrue(fuelPrices.ContainsKey(FuelType.Hydrogen));
            Assert.IsTrue(fuelPrices.ContainsKey(FuelType.LPG));
            Assert.IsTrue(fuelPrices.ContainsKey(FuelType.Unleaded));
            Assert.AreEqual(fuelPrices[FuelType.Diesel], wcfQuote.Where(x => x.Name == FuelType.Diesel.ToString()).FirstOrDefault().Price);
        }

        [TestMethod]
        public void GetFuelPrices_WithDataLessThan24HoursOldInCache_GetsDataFromCache()
        {
            var priceCache = new PriceCache();
            priceCache.SavePriceData(
                new Dictionary<FuelType, double>()
                {
                    {FuelType.Diesel, 5}
                },
                DateTime.Now.Subtract(new TimeSpan(0, 23, 59, 59)));
            var server = new FuelPricesServer(priceCache);

            var prices = server.GetFuelPrices();

            Assert.IsTrue(prices.ContainsKey(FuelType.Diesel));
            Assert.AreEqual(5, prices[FuelType.Diesel], double.Epsilon);
            Assert.IsFalse(prices.ContainsKey(FuelType.Hydrogen));
        }

        [TestMethod]
        public void GetFuelPrices_WithData24HoursOldInCache_GetsDataFromServer()
        {
            var priceCache = new PriceCache();
            priceCache.SavePriceData(
                new Dictionary<FuelType, double>()
                {
                    {FuelType.Diesel, 5}
                },
                DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0)));
            
            var server = new FuelPricesServer(priceCache);

            var prices = server.GetFuelPrices();
            
            Assert.IsTrue(prices.ContainsKey(FuelType.Hydrogen));
        }
    }
}
