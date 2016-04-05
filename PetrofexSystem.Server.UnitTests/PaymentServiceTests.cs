using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetrofexSystem.Common;
using PumpLibrary;

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
                IsPaid = true,
                LitresPumped = 1,
                PumpId = "Test",
                TotalAmount = 5
            };

            paymentService.SendForProcessing(transaction);

            Assert.AreEqual(transaction, db.LastTransaction);
        }
    }
}
