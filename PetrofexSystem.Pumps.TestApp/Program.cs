using PumpLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PetrofexSystem.Pumps.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var customerGenerator = new CustomerGenerator();
            var adapter = new CustomerGeneratorAdapter(customerGenerator);
            var activationServer = new LocalActivationServer();
            var fuelPricesServer = new LocalFuelPricesServer();
            var transactionServer = new LocalTransactionServer();
            
            var pump = new Pump(activationServer, adapter, fuelPricesServer, transactionServer, Guid.NewGuid().ToString());
            activationServer.RegisterPump(pump);

            var timer = new Timer(UpdateDisplay, pump, 0, 50);
            Console.ReadKey();
        }

        private static void UpdateDisplay(object state)
        {
            var pump = (Pump)state;
            Console.Clear();
            Console.WriteLine("Current transaction: {0}", pump.CurrentTransaction);
        }
    }
}
