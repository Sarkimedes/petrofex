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

            Assert.AreEqual(PumpStatus.CustomerWaiting, pumpManager.GetPumpStatus(pumpId));
        }

        [TestMethod]
        public void HandleActivationRequest_ForAlreadyActivePump_IgnoresActivationRequest()
        {
            var pumpManager = new PumpManager();
            var pumpId = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1).ToString();

            pumpManager.HandleActivationRequest(pumpId);
            pumpManager.ActivatePump(pumpId);
            pumpManager.HandleActivationRequest(pumpId);

            Assert.AreEqual(PumpStatus.ActivationPending, pumpManager.GetPumpStatus(pumpId));
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

            Assert.AreEqual(PumpStatus.Active, pumpManager.GetPumpStatus(pumpId));
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
                new Transaction()
                {
                    FuelType = FuelType.Diesel,
                    LitresPumped = 1,
                    TotalAmount = 1,
                    IsPaid = false
                },
                transaction);
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

            Assert.AreEqual(PumpStatus.AwaitingPayment, pumpManager.GetPumpStatus(pumpId));
        }

    }
}
