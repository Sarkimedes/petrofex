using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PetrofexSystem.PosTerminals.UnitTests
{
    [TestClass]
    public class PosTerminalTests
    {
        [TestMethod]
        public void ActivatePump_WithPumpIdForInactivePump_ShouldActivatePump()
        {            
            var pumpManager = new PumpManager();
            var posTerminal = new PosTerminal(pumpManager);
            var pumpId = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1).ToString();

            pumpManager.HandleActivationRequest(pumpId);
            posTerminal.ActivatePump(pumpId);

            Assert.AreEqual(PumpStatus.ActivationPending, pumpManager.GetPumpStatus(pumpId));
        }


        // Fail silently if activation is requested for an already active pump
        [TestMethod]
        public void ActivatePump_WithPumpIdForActivePump_ShouldDoNothing()
        {
            var pumpManager = new PumpManager();
            var posTerminal = new PosTerminal(pumpManager);
            var pumpId = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1).ToString();

            pumpManager.HandleActivationRequest(pumpId);
            posTerminal.ActivatePump(pumpId);
            posTerminal.ActivatePump(pumpId);

            Assert.AreEqual(PumpStatus.ActivationPending, posTerminal.GetPumpStatus(pumpId));
        }

        [TestMethod]
        public void ActivatePump_WithPumpIdActivatedAtADifferentTerminal_ShouldDoNothing()
        {
            var pumpManager = new PumpManager();
            var posTerminal = new PosTerminal(pumpManager);
            var posTerminal2 = new PosTerminal(pumpManager);
            var pumpId = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1).ToString();

            pumpManager.HandleActivationRequest(pumpId);
            posTerminal.ActivatePump(pumpId);
            posTerminal2.ActivatePump(pumpId);

            Assert.AreEqual(PumpStatus.ActivationPending, posTerminal.GetPumpStatus(pumpId));
            Assert.AreEqual(PumpStatus.ActivationPending, posTerminal2.GetPumpStatus(pumpId));
        }

        [TestMethod]
        public void ActivatePump_WithPumpIdForInactivePump_ShouldOnlyRequestToActivateThePumpMatchingThatId()
        {
            var pumpManager = new PumpManager();
            var posTerminal = new PosTerminal(pumpManager);
            var pumpId = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1).ToString();
            var pumpId2 = new Guid(2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2).ToString();
            pumpManager.HandleActivationRequest(pumpId);
            pumpManager.HandleActivationRequest(pumpId2);

            posTerminal.ActivatePump(pumpId);

            Assert.AreEqual(PumpStatus.ActivationPending, posTerminal.GetPumpStatus(pumpId));
            Assert.AreEqual(PumpStatus.CustomerWaiting, posTerminal.GetPumpStatus(pumpId2));
        }

        [TestMethod]
        public void PumpIsActive_WithPumpIdForPumpActivatedAtADifferentPosTerminal_ShouldReportThatActivationWasRequestedForThePump()
        {
            var pumpManager = new PumpManager();
            var posTerminal = new PosTerminal(pumpManager);
            var posTerminal2 = new PosTerminal(pumpManager);
            var pumpId = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1).ToString();
            pumpManager.HandleActivationRequest(pumpId);

            posTerminal.ActivatePump(pumpId);
            
            Assert.AreEqual(PumpStatus.ActivationPending, posTerminal2.GetPumpStatus(pumpId));
        }
    }
}
