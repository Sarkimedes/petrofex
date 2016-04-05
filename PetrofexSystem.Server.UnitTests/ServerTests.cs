using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PetrofexSystem.Server.UnitTests
{
    [TestClass]
    public class ServerTests
    {
        [TestMethod]
        public void RequestActivation_WithValidPump_RaisesEvent()
        {
            var server = new Server();
            var activated = false;
            server.PumpActivationRequested += s => activated = true;

            server.RequestActivation("test");

            Assert.IsTrue(activated);
        }

        [TestMethod] 
        public void RequestDeactivation_WithActivePump_RaisesEvent()
        {
            var server = new Server();
            var deactivated = false;
            server.PumpDeactivationRequested += s => deactivated = true;

            server.RequestDeactivation("test");

            Assert.IsTrue(deactivated);
        }
    }
}
