using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
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

        [TestMethod]
        public void Activate_WithValidTransaction_SetsCurrentTransactionToNextTransaction()
        {
            var customerGenerator = new FakeCustomerGenerator();
            var server = new FakeServer();
            var pump = new Pump(server, customerGenerator);

            customerGenerator.InvokeCustomerReady(new CustomerReadyEventArgs(FuelType.Diesel));
            pump.Activate();

            var currentTransaction = pump.CurrentTransaction;
            var expectedTransaction = new FuelTransaction()
            {
                FuelType = FuelType.Diesel,
                Total = 0
            };
            Assert.AreEqual(expectedTransaction, currentTransaction);
        }

        [TestMethod]
        public void Activate_WithValidTransaction_ActivatesPump()
        {
            var customerGenerator = new FakeCustomerGenerator();
            var server = new FakeServer();
            var pump = new Pump(server, customerGenerator);

            customerGenerator.InvokeCustomerReady(new CustomerReadyEventArgs(FuelType.Diesel));
            pump.Activate();

            Assert.IsTrue(customerGenerator.IsPumpActive);
        }

        [TestMethod]
        public void PumpProgress_AfterActivation_IncrementsCurrentTransaction()
        {
            var customerGenerator = new FakeCustomerGenerator();
            var server = new FakeServer();
            var pump = new Pump(server, customerGenerator);

            customerGenerator.InvokeCustomerReady(new CustomerReadyEventArgs(FuelType.Diesel));
            pump.Activate();
            customerGenerator.InvokePumpProgress(new PumpProgressEventArgs(1));

            Assert.AreEqual(new FuelTransaction() {FuelType = FuelType.Diesel, Total = 1}, pump.CurrentTransaction);
        }
    }
}
