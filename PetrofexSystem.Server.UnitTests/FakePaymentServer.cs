using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using PetrofexSystem.Common;
using PetrofexSystem.Server;

namespace PetrofexSystem.PosTerminals.UnitTests
{
    internal class FakePaymentServer : IPaymentServer
    {
        public Transaction LastTransaction { get; private set; }

        public void SendForProcessing(Transaction transaction)
        {
            this.LastTransaction = transaction;
        }
    }
}
