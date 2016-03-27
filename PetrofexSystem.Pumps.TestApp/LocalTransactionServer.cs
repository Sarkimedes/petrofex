using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetrofexSystem.Common;

namespace PetrofexSystem.Pumps.TestApp
{
    internal class LocalTransactionServer : ITransactionServer
    {
        public void SendFuelTransaction(Transaction transaction)
        {
            // Do nothing but write to console
            Console.WriteLine("Transaction sent to server: {0}", transaction);
        }
    }
}
