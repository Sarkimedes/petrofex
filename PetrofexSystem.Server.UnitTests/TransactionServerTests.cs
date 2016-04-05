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
    public class TransactionServerTests
    {
        [TestMethod]
        public void SendTransaction_WithValidTransaction_RaisesEvent()
        {
            var transaction = new Transaction()
            {
                FuelType = FuelType.Hydrogen,
                LitresPumped = 1,
                IsPaid = false,
                PumpId = "test",
                TotalAmount = 5
            };

            var received = false;

            var service = new TransactionService();
            service.TransactionReceived += t => received = true;
            service.SendFuelTransaction(transaction);

            Assert.IsTrue(received);
        }
    }
}
