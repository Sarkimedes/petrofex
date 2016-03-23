using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PumpLibrary;

namespace PetrofexSystem.Pumps.TestApp
{
    class LocalFuelPricesServer : IFuelPricesServer
    {
        public IDictionary<PumpLibrary.FuelType, double> GetFuelPrices()
        {
            return new Dictionary<PumpLibrary.FuelType, double>() 
            {
                { FuelType.Diesel, 1 },
                { FuelType.Hydrogen, 2},
                { FuelType.LPG, 3},
                { FuelType.Unleaded, 4}
            };
        }
    }
}
