using PumpLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PetrofexSystem.Server;

namespace PetrofexSystem.Pumps.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var customerGenerator = new CustomerGenerator();
            var adapter = new CustomerGeneratorAdapter(customerGenerator);
            var id = Guid.NewGuid().ToString();
            var messagingClient = new MessagingClient(id);
            var activationServer = new PumpActivationServer(messagingClient);
            var fuelPricesServer = new LocalFuelPricesServer();
            var transactionServer = new TransactionServer(messagingClient);
            
            var pump = new Pump(activationServer, adapter, fuelPricesServer, transactionServer, messagingClient, Guid.NewGuid().ToString());
            customerGenerator.Start();
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
