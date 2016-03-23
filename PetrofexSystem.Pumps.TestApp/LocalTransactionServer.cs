using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetrofexSystem.Pumps.TestApp
{
    class LocalTransactionServer : ITransactionServer
    {
        public void SendFuelTransaction(FuelTransaction transaction)
        {
            // Do nothing but write to console
            Console.WriteLine("Transaction sent to server: {0}", transaction);
        }
    }
}
