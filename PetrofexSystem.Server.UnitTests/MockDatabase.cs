using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetrofexSystem.Common;

namespace PetrofexSystem.Server.UnitTests
{
    class MockDatabase : IPaymentDatabase
    {
        public Transaction LastTransaction { get; private set; }

        public void SaveTransaction(Transaction transaction)
        {
            this.LastTransaction = transaction;
        }
    }
}
