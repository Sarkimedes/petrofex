using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetrofexSystem.PricesServer.Client.FuelSupplyService;
using PumpLibrary;

namespace PetrofexSystem.Server
{
    public class PriceCache
    {
        private IDictionary<FuelType, double> _fuelPrices; 

        public DateTime LastSaved { get; private set; }
        public void SavePriceData(IDictionary<FuelType, double> prices, DateTime timestamp)
        {
            this._fuelPrices = prices;
            LastSaved = timestamp;
        }

        public IDictionary<FuelType, double> GetSavedPrices()
        {
            return this._fuelPrices;
        }
    }
}
