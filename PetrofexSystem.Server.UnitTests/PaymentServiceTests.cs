using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetrofexSystem.Common;
using PumpLibrary;
using PetrofexSystem.Server;

namespace PetrofexSystem.Server.UnitTests
{
    [TestClass]
    public class PaymentServiceTests
    {
        [TestMethod]
        public void SendForProcessing_WithValidTransaction_SendsDataToDatabase()
        {
            var db = new MockDatabase();
            var paymentService = new PaymentService(db);
            var transaction = new Transaction()
            {
                FuelType = FuelType.Diesel,
                LitresPumped = 1,
                PumpId = "Test",
                TotalAmount = 5
            };

            paymentService.SendForProcessing(transaction, () => { });

            Assert.AreEqual(transaction, db.LastTransaction);
        }

        [TestMethod]
        public void ProcessTransaction_WithValidTransaction_ExecutesCallbackWithPumpId()
        {
            var db = new MockDatabase();
            var paymentService = new PaymentService(db);
            var transaction = new Transaction()
            {
                FuelType = FuelType.Diesel,
                LitresPumped = 1,
                PumpId = "Test",
                TotalAmount = 5
            };
            var idUsed = string.Empty;

            paymentService.ProcessTransaction(transaction, x => idUsed = x);

            Assert.AreEqual(idUsed, transaction.PumpId);
        }
    }
}
