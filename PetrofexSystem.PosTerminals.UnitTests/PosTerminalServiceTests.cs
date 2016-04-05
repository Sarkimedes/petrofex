using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PetrofexSystem.PosTerminals.UnitTests
{
    /// <summary>
    /// Summary description for PosTerminalServiceTests
    /// </summary>
    [TestClass]
    public class PosTerminalServiceTests
    {
        [TestMethod]
        public void HandlePumpActivationRequest_WithPumpId_ChangesPumpState()
        {
            var id = "test";
            var factory = new MockPumpFactory();
            var pump = new Pump(id, new FakePaymentServer(), new FakeStateManager());
            factory.AddPump(pump);
            var posService = new PosTerminalService(factory);

            posService.HandlePumpActivationRequest(id);

            Assert.AreEqual(PumpState.ActivationPending, pump.CurrentState);
        }

        [TestMethod]
        public void HandlePumpDeactivationRequest_WithActivePump_DeactivatesPump()
        {
            var id = "test";
            var factory = new MockPumpFactory();
            var stateManager = new FakeStateManager();
            var pump = new Pump(id, new FakePaymentServer(), stateManager);
            factory.AddPump(pump);
            var posService = new PosTerminalService(factory);
            stateManager.SetState(PumpState.Active);

            posService.HandlePumpDeactivationRequest(id);

            Assert.AreEqual(PumpState.AwaitingPayment, pump.CurrentState);
        }
    }
}
