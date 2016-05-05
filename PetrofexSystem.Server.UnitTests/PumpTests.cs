using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetrofexSystem.Common;
using PetrofexSystem.Server;
using PumpLibrary;

namespace PetrofexSystem.PosTerminals.UnitTests
{
    [TestClass]
    public class PumpTests
    {


        [TestMethod]
        public void HandlePumpProgress_ForActivePump_UpdatesTransactionForThatPump()
        {
            var pumpId = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1).ToString();
            var pump = CreatePumpWithId(pumpId);
            pump.Activate();

            pump.HandlePumpProgress(new Transaction()
            {
                PumpId = pumpId,
                FuelType = FuelType.Diesel,
                LitresPumped = 1,
                TotalAmount = 1
            });

            var transaction = pump.CurrentTransaction;
            Assert.AreEqual(
                new Transaction()
                {
                    PumpId = pumpId,
                    FuelType = FuelType.Diesel,
                    LitresPumped = 1,
                    TotalAmount = 1,
                },
                transaction);
        }

        [TestMethod]
        public void HandlePumpProgress_CalledTwiceOnActivePump_UpdatesTransactionForThatPump()
        {
            var pumpId = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1).ToString();
            var pump = CreatePumpWithId(pumpId);
            pump.Activate();

            pump.HandlePumpProgress(new Transaction()
            {
                PumpId = pumpId,
                FuelType = FuelType.Diesel,
                LitresPumped = 1,
                TotalAmount = 1
            });
            pump.HandlePumpProgress(new Transaction()
            {
                PumpId = pumpId,
                FuelType = FuelType.Diesel,
                LitresPumped = 2,
                TotalAmount = 2
            });

            var latestTransaction = pump.CurrentTransaction;
            var expectedTransaction = new Transaction()
            {
                PumpId = pumpId,
                FuelType = FuelType.Diesel,
                LitresPumped = 2,
                TotalAmount = 2,
            };
            Assert.AreEqual(expectedTransaction, latestTransaction);
        }


        private Pump CreatePumpWithId(string id)
        {
            var pumpFactory = new PumpFactory(new FakePaymentServer());
            return pumpFactory.GetPumpById(id);
        }

        private Pump CreateNewPump()
        {
            return this.CreatePumpWithId(Guid.NewGuid().ToString());
        }

        [TestMethod]
        public void HandlePaymentAcknowledged_ForPumpWithValidPayment_UpdatesPaymentFinished()
        {
            var pump = this.CreateNewPump();
            pump.Activate();
            pump.HandlePumpProgress(new Transaction());
            pump.Deactivate();
            pump.PayCurrentTransaction();

            pump.HandlePaymentAcknowledged();

            Assert.IsTrue(pump.TransactionPaid);
        }
    }
}
