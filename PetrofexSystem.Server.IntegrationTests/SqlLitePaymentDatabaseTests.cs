using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetrofexSystem.Server.DbModels;
using PumpLibrary;

namespace PetrofexSystem.Server.IntegrationTests
{
    [TestClass]
    public class SqlLitePaymentDatabaseTests
    {
        [TestMethod]
        public void SaveTransaction_SavesTransactionInDB_WithTransactionName()
        {
            using (var context = new PaymentsContext())
            {
                context.Transactions.RemoveRange(context.Transactions);
                context.SaveChanges();
            }
            var transaction = new Common.Transaction()
            {
                FuelType = FuelType.Diesel,
                LitresPumped = 5.0,
                PumpId = "Test",
                TotalAmount = 7.0
            };

            var sut = new SqlLitePaymentDatabase();
            sut.SaveTransaction(transaction);

            using (var context = new PaymentsContext())
            {
                Assert.AreEqual(1, context.Transactions.Count());
            }
        }
    }
}
