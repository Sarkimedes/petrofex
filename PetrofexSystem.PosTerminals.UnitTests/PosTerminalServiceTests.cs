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
            var posService = new PosTerminalService(factory);

            posService.HandlePumpActivationRequest(id);

            var pump = factory.pumpsCreated.First(x => x.Id == id);
            Assert.AreEqual(PumpState.CustomerWaiting, pump.CurrentState);
        }
    }
}
