using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PetrofexSystem.PosTerminals.UnitTests
{
    /// <summary>
    /// Summary description for PumpStateManagerTests
    /// </summary>
    [TestClass]
    public class PumpStateManagerTests
    {
        [TestMethod]
        public void Constructor_ShouldSetStateToInactive()
        {
            var stateManager = new PumpStateManager();

            Assert.AreEqual(PumpState.Inactive, stateManager.CurrentState);
        }

        [TestMethod]
        public void SetState_FromInactiveToCustomerWaiting_ShouldSetStateToCustomerWaiting()
        {
            var stateManager = new PumpStateManager();

            stateManager.SetState(PumpState.CustomerWaiting);

            Assert.AreEqual(PumpState.CustomerWaiting, stateManager.CurrentState);
        }

        [TestMethod]
        public void SetState_FromInactiveToInactive_ShouldSetStateToInactive()
        {
            var stateManager = new PumpStateManager();

            stateManager.SetState(PumpState.Inactive);

            Assert.AreEqual(PumpState.Inactive, stateManager.CurrentState);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SetState_FromInactiveToActivationPending_ShouldThrowError()
        {
            var stateManager = new PumpStateManager();

            stateManager.SetState(PumpState.ActivationPending);
        }
    }
}
