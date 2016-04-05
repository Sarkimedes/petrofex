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
        public IDictionary<FuelType, double> GetFuelPrices()
        {
            var client = new FuelSupplyServiceClient();
            var quote = client.GetFuelPrices(1).QuotePrices;

            var prices = new Dictionary<FuelType, double>();

            foreach (var fuelPrice in quote)
            {
                FuelType fuelType;
                if (Enum.TryParse(fuelPrice.Name, out fuelType))
                {
                    prices.Add(fuelType, fuelPrice.Price);
                }
            }

            return prices;
        }
    }
}
