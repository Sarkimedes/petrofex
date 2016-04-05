using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetrofexSystem.Common;

namespace PetrofexSystem.Server
{
    public static class PaymentServiceExtensions
    {
        public static void ProcessTransaction(
            this IPaymentServer transactionServer,
            Transaction transaction,
            Action<string> finishedCallback)
        {
            transactionServer.SendForProcessing(transaction);
            finishedCallback(transaction.PumpId);
        }
    }
}
