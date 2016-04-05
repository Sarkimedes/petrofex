using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetrofexSystem.PricesServer.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new FuelSupplyService.FuelSupplyServiceClient();
            var quote = client.GetFuelPrices(1);
            var prices = quote.QuotePrices;
            Console.WriteLine("Prices obtained at {0}", quote.QuoteDate);
            foreach (var fuelPrice in prices)
            {
                Console.WriteLine("{0} | {1}", fuelPrice.Name, fuelPrice.Price);
            }
            Console.ReadKey();
        }
    }
}
