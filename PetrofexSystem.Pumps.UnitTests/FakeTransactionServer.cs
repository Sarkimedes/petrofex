using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetrofexSystem.Pumps.UnitTests
{
    internal class FakeTransactionServer : ITransactionServer
    {
        internal FuelTransaction LastTransaction { get; private set; }

        public void SendFuelTransaction(FuelTransaction transaction)
        {
            this.LastTransaction = transaction;
        }

        public void Reset()
        {
            this.LastTransaction = default(FuelTransaction);
        }
    }
}
