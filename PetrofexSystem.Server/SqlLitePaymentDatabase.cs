using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetrofexSystem.Server.DbModels;
using Transaction = PetrofexSystem.Common.Transaction;

namespace PetrofexSystem.Server
{
    public class SqlLitePaymentDatabase : IPaymentDatabase
    {
        public void SaveTransaction(Transaction transaction)
        {
            using (var context = new PaymentsContext())
            {
                var savedTransaction = new DbModels.Transaction()
                {
                    FuelType = transaction.FuelType.ToString(),
                    Amount = transaction.TotalAmount,
                    LitresPumped = transaction.LitresPumped,
                    PumpId = transaction.PumpId
                };
                context.Transactions.Add(savedTransaction);
                context.SaveChanges();
            }
        }
    }
}
