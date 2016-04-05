using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetrofexSystem.Common;

namespace PetrofexSystem.Server
{
    public class TransactionService : ITransactionServer 
    {
        public void SendFuelTransaction(Transaction transaction)
        {
            if (this.TransactionReceived != null)
            {
                this.TransactionReceived(transaction);
            }
        }

        public event Action<Transaction> TransactionReceived;
    }
}
