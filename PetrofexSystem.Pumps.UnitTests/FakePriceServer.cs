using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PumpLibrary;

namespace PetrofexSystem.Pumps.UnitTests
{
    class FakePriceServer : IFuelPricesServer
    {
        public IDictionary<FuelType, double> GetFuelPrices()
        {
            var prices = new Dictionary<FuelType, double>
            {
                {FuelType.Diesel, 1},
                {FuelType.Hydrogen, 2},
                {FuelType.LPG, 2.5},
                {FuelType.Unleaded, 3}
            };
            return prices;
        }
    }
}
