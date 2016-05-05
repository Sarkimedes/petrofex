using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetrofexSystem.Common;

namespace PetrofexSystem.Server
{
    public class PaymentService : IPaymentServer
    {
        private readonly IPaymentDatabase _database;

        public PaymentService(IPaymentDatabase database)
        {
            if (database == null)
            {
                throw new ArgumentNullException("database");
            }

            this._database = database;
        }



        public void SendForProcessing(Transaction transaction, Action onSuccess)
        {
            this._database.SaveTransaction(transaction);
        }
    }
}
