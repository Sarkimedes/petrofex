using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetrofexSystem.PricesServer.Client.FuelSupplyService;
using PumpLibrary;

namespace PetrofexSystem.Server
{
    public class FuelPricesServer : IFuelPricesServer
    {
        private readonly PriceCache _cache;

        public FuelPricesServer(PriceCache cache)
        {
            this._cache = cache;
        }

        public IDictionary<FuelType, double> GetFuelPrices()
        {
            // If the cached prices are less than 24 hours old
            if (this._cache.LastSaved > DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0)))
            {
                // Return prices from cache
                return this._cache.GetSavedPrices();
            }

            var client = new FuelSupplyServiceClient();
            var quote = client.GetFuelPrices(1);
            var quotePrices = quote.QuotePrices;

            var prices = new Dictionary<FuelType, double>();

            foreach (var fuelPrice in quotePrices)
            {
                FuelType fuelType;
                if (Enum.TryParse(fuelPrice.Name, out fuelType))
                {
                    prices.Add(fuelType, fuelPrice.Price);
                }
            }

            this._cache.SavePriceData(prices, quote.QuoteDate);

            return prices;
        }
    }
}
