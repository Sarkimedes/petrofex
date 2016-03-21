using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PumpLibrary;

namespace PetrofexSystem.Pumps.UnitTests
{
    [TestClass]
    public class PumpTests
    {
        [TestMethod]
        public void Pump_CustomerWaiting_SendsActivateRequestToServer()
        {
            var customerGenerator = new FakeCustomerGenerator();
            var server = new FakeServer();
            var pump = new Pump(server, customerGenerator);
            
            customerGenerator.InvokeCustomerReady(new CustomerReadyEventArgs(FuelType.Diesel));

            Assert.IsTrue(server.ActivationRequested);
        }

        [TestMethod]
        public void Pump_CustomerReady_SetsNextTransactionToSuppliedFuelType()
        {
            var customerGenerator = new FakeCustomerGenerator();
            var server = new FakeServer();
            var pump = new Pump(server, customerGenerator);

            customerGenerator.InvokeCustomerReady(new CustomerReadyEventArgs(FuelType.Diesel));

            Assert.AreEqual(pump.NextTransaction.FuelType, FuelType.Diesel);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CustomerReady_WithNoEventArgs_ThrowsArgumentNullException()
        {
            var customerGenerator = new FakeCustomerGenerator();
            var server = new FakeServer();
            var pump = new Pump(server, customerGenerator);

            customerGenerator.InvokeCustomerReady(null);
        }
    }
}
