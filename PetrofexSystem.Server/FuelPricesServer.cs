using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PumpLibrary;

namespace PetrofexSystem.Server
{
    public class FuelPricesServer : IFuelPricesServer
    {
        public IDictionary<FuelType, double> GetFuelPrices()
        {
            return new Dictionary<FuelType, double>()
            {
                {FuelType.Diesel, 5},
                {FuelType.Hydrogen, 5},
                {FuelType.LPG, 5},
                {FuelType.Unleaded, 5}
            };
        }
    }
}
