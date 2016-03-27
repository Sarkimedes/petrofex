using System;
using System.Reflection.Emit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PumpLibrary;

namespace PetrofexSystem.PosTerminals.UnitTests
{
    [TestClass]
    public class PumpManagerTests
    {
        [TestMethod]
        public void GetPumpStatus_AfterHandlingAnActivationRequestForANewPump_ShouldReportThatPumpHasACustomerWaiting()
        {
            var pumpManager = new PumpManager();
            var pumpId = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1).ToString();

            pumpManager.HandleActivationRequest(pumpId);

            Assert.AreEqual(PumpState.CustomerWaiting, pumpManager.GetPumpStatus(pumpId));
        }

        [TestMethod]
        public void HandleActivationRequest_ForAlreadyActivePump_IgnoresActivationRequest()
        {
            var pumpManager = new PumpManager();
            var pumpId = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1).ToString();

            pumpManager.HandleActivationRequest(pumpId);
            pumpManager.ActivatePump(pumpId);
            pumpManager.HandleActivationRequest(pumpId);

            Assert.AreEqual(PumpState.ActivationPending, pumpManager.GetPumpStatus(pumpId));
        }

        // Test handling for progress update 
        [TestMethod]
        public void HandlePumpProgress_ForActivePump_UpdatesPumpStatusToActive()
        {
            var pumpManager = new PumpManager();
            var pumpId = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1).ToString();

            pumpManager.HandleActivationRequest(pumpId);
            pumpManager.ActivatePump(pumpId);
            pumpManager.HandlePumpProgress(pumpId, 0, 0, 0);

            Assert.AreEqual(PumpState.Active, pumpManager.GetPumpStatus(pumpId));
        }

        [TestMethod]
        public void HandlePumpProgress_ForActivePump_UpdatesTransactionForThatPump()
        {
            var pumpManager = new PumpManager();
            var pumpId = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1).ToString();
            pumpManager.HandleActivationRequest(pumpId);
            pumpManager.ActivatePump(pumpId);

            pumpManager.HandlePumpProgress(pumpId, FuelType.Diesel, 1, 1);

            var transaction = pumpManager.GetLatestTransaction(pumpId);
            Assert.AreEqual(
                new Common.Transaction()
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
            var pumpManager = new PumpManager();
            var pumpId = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1).ToString();
            pumpManager.HandleActivationRequest(pumpId);
            pumpManager.ActivatePump(pumpId);

            pumpManager.HandlePumpProgress(pumpId, FuelType.Diesel, 1, 1);
            pumpManager.HandlePumpProgress(pumpId, FuelType.Diesel, 2, 2);

            var latestTransaction = pumpManager.GetLatestTransaction(pumpId);
            var expectedTransaction = new Common.Transaction()
            {
                PumpId = pumpId,
                FuelType = FuelType.Diesel,
                LitresPumped = 2,
                TotalAmount = 2,
                IsPaid = false
            };
            Assert.AreEqual(expectedTransaction, latestTransaction);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void HandlePumpProgress_ForNonExistentPump_ThrowsException()
        {
            var pumpManager = new PumpManager();
            var pumpId = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1).ToString();

            pumpManager.HandlePumpProgress(pumpId, 0, 0, 0);
        }

        // Check that state changes to mark pump as awaiting payment when pumping is finished
        [TestMethod]
        public void HandleDeactivationRequest_ForActivePump_UpdatesPumpStatusToAwaitingPayment()
        {
            var pumpManager = new PumpManager();
            var pumpId = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1).ToString();
            pumpManager.HandleActivationRequest(pumpId);
            pumpManager.HandlePumpProgress(pumpId, 0, 0, 0);

            pumpManager.HandleDeactivationRequest(pumpId);

            Assert.AreEqual(PumpState.AwaitingPayment, pumpManager.GetPumpStatus(pumpId));
        }

        // Check that state changes to mark pump as payment made when pumping is finished
        [TestMethod]
        public void MakePayment_ForActivePump_UpdatesPumpStatusToPaymentMade()
        {
            var pumpManager = new PumpManager();
            var pumpId = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1).ToString();
            pumpManager.HandleActivationRequest(pumpId);
            pumpManager.HandlePumpProgress(pumpId, 0, 0, 0);
            pumpManager.HandleDeactivationRequest(pumpId);

            pumpManager.SubmitPayment(pumpId);

            Assert.AreEqual(PumpState.PaymentMade, pumpManager.GetPumpStatus(pumpId));
        }

        // Check that state changes to mark pump as inactive when payment is acknowledged
        [TestMethod]
        public void ReceivePaymentAcknowledged_ForActivePump_UpdatesPumpStatusToInactive()
        {
            var pumpManager = new PumpManager();
            var pumpId = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1).ToString();
            pumpManager.HandleActivationRequest(pumpId);
            pumpManager.HandlePumpProgress(pumpId, 0, 0, 0);
            pumpManager.HandleDeactivationRequest(pumpId);
            pumpManager.SubmitPayment(pumpId);

            pumpManager.ReceivePaymentAcknowledged(pumpId);

            Assert.AreEqual(PumpState.Inactive, pumpManager.GetPumpStatus(pumpId));
        }

    }
}
