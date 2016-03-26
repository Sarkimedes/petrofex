using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            pumpManager.HandlePumpProgress(pumpId);

            Assert.AreEqual(PumpStatus.Active, pumpManager.GetPumpStatus(pumpId));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void HandlePumpProgress_ForNonExistentPump_ThrowsException()
        {
            var pumpManager = new PumpManager();
            var pumpId = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1).ToString();

            pumpManager.HandlePumpProgress(pumpId);
        }
    }
}
