using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetrofexSystem.Common;

namespace PetrofexSystem.Pumps.UnitTests
{
    internal class FakeTransactionServer : ITransactionServer
    {
        internal Transaction LastTransaction { get; private set; }

        public void SendFuelTransaction(Transaction transaction)
        {
            this.LastTransaction = transaction;
        }

        public void Reset()
        {
            this.LastTransaction = default(Transaction);
        }
    }
}
