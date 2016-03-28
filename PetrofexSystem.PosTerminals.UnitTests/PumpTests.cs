using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetrofexSystem.Common;
using PumpLibrary;

namespace PetrofexSystem.PosTerminals.UnitTests
{
    [TestClass]
    public class PumpTests
    {
        [TestMethod]
        public void HandleActivationRequest_ForAlreadyActivePump_IgnoresActivationRequest()
        {
            var pumpId = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1).ToString();
            var factory = new PumpFactory(new FakePaymentServer());
            var pump = factory.HandleActivationRequest(pumpId);
            pump.Activate();

            pump = factory.HandleActivationRequest(pumpId);

            Assert.AreEqual(PumpState.ActivationPending, pump.CurrentState);            
        }

        // Test handling for progress update 
        [TestMethod]
        public void HandlePumpProgress_ForPumpWithPendingActivation_UpdatesPumpStatusToActive()
        {
            var pump = this.CreateNewPump();
            pump.Activate();

            pump.HandlePumpProgress(new Transaction());

            Assert.AreEqual(PumpState.Active, pump.CurrentState);
        }

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
                    IsPaid = false
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
                IsPaid = false
            };
            Assert.AreEqual(expectedTransaction, latestTransaction);
        }

        // Check that state changes to mark pump as awaiting payment when pumping is finished
        [TestMethod]
        public void HandleDeactivationRequest_ForActivePump_UpdatesPumpStatusToAwaitingPayment()
        {
            var pump = this.CreateNewPump();
            pump.Activate();
            pump.HandlePumpProgress(new Transaction());

            pump.Deactivate();

            Assert.AreEqual(PumpState.AwaitingPayment, pump.CurrentState);
        }

        // Check that state changes to mark pump as payment made when pumping is finished
        [TestMethod]
        public void PayCurrentTransaction_ForActivePump_UpdatesPumpStatusToPaymentMade()
        {
            var pump = this.CreateNewPump();
            pump.Activate();
            pump.HandlePumpProgress(new Transaction());
            pump.Deactivate();

            pump.PayCurrentTransaction();

            Assert.AreEqual(PumpState.PaymentMade, pump.CurrentState);
        }

        [TestMethod]
        public void PayCurrentTransaction_WithValidTransaction_SendsTransactionToTheServer()
        {
            var pumpId = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1).ToString();
            var paymentServer = new FakePaymentServer();
            var pumpFactory = new PumpFactory(paymentServer);
            var pump = pumpFactory.HandleActivationRequest(pumpId);
            pump.Activate();
            pump.HandlePumpProgress(new Transaction()
            {
                FuelType = FuelType.Hydrogen,
                IsPaid = false,
                LitresPumped = 1,
                TotalAmount = 2,
                PumpId = pumpId
            });
            pump.Deactivate();
            
            pump.PayCurrentTransaction();

            Assert.AreEqual(pump.CurrentTransaction, paymentServer.LastTransaction);
        }

        private Pump CreatePumpWithId(string id)
        {
            var pumpFactory = new PumpFactory(new FakePaymentServer());
            return pumpFactory.HandleActivationRequest(id);
        }

        private Pump CreateNewPump()
        {
            return this.CreatePumpWithId(Guid.NewGuid().ToString());
        }

        // Check transaction is not already paid before attempting to send it.

        // Check that state changes to mark pump as inactive when payment is acknowledged
        [TestMethod]
        public void ReceivePaymentAcknowledged_ForActivePump_UpdatesPumpStatusToInactive()
        {
            var pump = this.CreateNewPump();
            pump.Activate();
            pump.HandlePumpProgress(new Transaction());
            pump.Deactivate();
            pump.PayCurrentTransaction();

            pump.HandlePaymentAcknowledged();
            
            Assert.AreEqual(PumpState.Inactive, pump.CurrentState);
        }

        [TestMethod]
        public void HandlePaymentAcknowledged_ForPumpWithValidPayment_UpdatesCurrentTransactionToPaid()
        {
            var pump = this.CreateNewPump();
            pump.Activate();
            pump.HandlePumpProgress(new Transaction());
            pump.Deactivate();
            pump.PayCurrentTransaction();

            pump.HandlePaymentAcknowledged();

            Assert.IsTrue(pump.CurrentTransaction.IsPaid);
        }
    }
}
