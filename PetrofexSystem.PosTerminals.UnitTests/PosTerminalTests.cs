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
            var posTerminal = new PosTerminal();
            var pumpId = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1).ToString();

            posTerminal.ActivatePump(pumpId);

            Assert.IsTrue(PumpHasPendingActivation(posTerminal, pumpId));
        }


        // Fail silently if activation is requested for an already active pump
        [TestMethod]
        public void ActivatePump_WithPumpIdForActivePump_ShouldDoNothing()
        {
            var posTerminal = new PosTerminal();
            var pumpId = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1).ToString();

            posTerminal.ActivatePump(pumpId);
            posTerminal.ActivatePump(pumpId);

            Assert.IsTrue(posTerminal.PumpIsActive(pumpId));
        }

        [TestMethod]
        public void ActivatePump_WithPumpIdActivatedAtADifferentTerminal_ShouldDoNothing()
        {
            var posTerminal = new PosTerminal();
            var posTerminal2 = new PosTerminal();
            var pumpId = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1).ToString();

            posTerminal.ActivatePump(pumpId);
            posTerminal2.ActivatePump(pumpId);

            Assert.IsTrue(posTerminal.PumpIsActive(pumpId));
            Assert.IsTrue(posTerminal2.PumpIsActive(pumpId));
        }

        [TestMethod]
        public void ActivatePump_WithPumpIdForInactivePump_ShouldOnlyActivateThePumpMatchingThatId()
        {
            var posTerminal = new PosTerminal();
            var pumpId = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1).ToString();
            var pumpId2 = new Guid(2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2).ToString();

            posTerminal.ActivatePump(pumpId);

            Assert.IsTrue(posTerminal.PumpIsActive(pumpId));
            Assert.IsFalse(posTerminal.PumpIsActive(pumpId2));
        }

        [TestMethod]
        public void PumpIsActive_WithPumpIdForPumpActivatedAtADifferentPosTerminal_ShouldReportThatThePumpWasActivated()
        {
            var posTerminal = new PosTerminal();
            var posTerminal2 = new PosTerminal();
            var pumpId = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1).ToString();

            posTerminal.ActivatePump(pumpId);
            
            Assert.IsTrue(posTerminal2.PumpIsActive(pumpId));
        }

        [TestMethod]
        public void PumpIsActive_WithPumpIdForNewPump_ShouldReportThatPumpHasACustomerWaiting()
        {
            var pumpManager = new PumpManager();
            var posTerminal = new PosTerminal(pumpManager);
            var pumpId = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1).ToString();

            pumpManager.HandleActivationRequest(pumpId);

            Assert.AreEqual(posTerminal.GetPumpStatus(pumpId), PumpStatus.CustomerWaiting);
        }

        private static bool PumpHasPendingActivation(PosTerminal posTerminal, string pumpId)
        {
            return posTerminal.PumpIsActive(pumpId);
        }
    }
}
