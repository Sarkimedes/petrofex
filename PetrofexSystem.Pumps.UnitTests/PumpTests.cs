using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetrofexSystem.Common;
using PetrofexSystem.Server;
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
            var server = new FakeActivationServer();
            var fuelPricesServer = new FakePriceServer();
            var transactionServer = new FakeTransactionServer();
            var pump = new Pump(server, customerGenerator, fuelPricesServer, transactionServer);

            customerGenerator.InvokeCustomerReady(new CustomerReadyEventArgs(FuelType.Diesel));

            Assert.IsTrue(server.ActivationRequested);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CustomerReady_WithNoEventArgs_ThrowsArgumentNullException()
        {
            var customerGenerator = new FakeCustomerGenerator();
            var server = new FakeActivationServer();
            var fuelPricesServer = new FakePriceServer();
            var transactionServer = new FakeTransactionServer();
            var pump = new Pump(server, customerGenerator, fuelPricesServer, transactionServer);

            customerGenerator.InvokeCustomerReady(null);
        }

        [TestMethod]
        public void Activate_WithValidTransaction_ActivatesPump()
        {
            var customerGenerator = new FakeCustomerGenerator();
            var server = new FakeActivationServer();
            var fuelPricesServer = new FakePriceServer();
            var transactionServer = new FakeTransactionServer();
            var pump = new Pump(server, customerGenerator, fuelPricesServer, transactionServer);

            customerGenerator.InvokeCustomerReady(new CustomerReadyEventArgs(FuelType.Diesel));
            pump.Activate();

            Assert.IsTrue(pump.IsActive);
        }

        [TestMethod]
        public void PumpProgress_AfterActivation_IncrementsCurrentTransaction()
        {
            var customerGenerator = new FakeCustomerGenerator();
            var server = new FakeActivationServer();
            var fuelPricesServer = new FakePriceServer();
            var transactionServer = new FakeTransactionServer();
            var pump = new Pump(server, customerGenerator, fuelPricesServer, transactionServer);

            customerGenerator.InvokeCustomerReady(new CustomerReadyEventArgs(FuelType.Diesel));
            pump.Activate();
            customerGenerator.InvokePumpProgress(new PumpProgressEventArgs(1));

            Assert.AreEqual(new Transaction() {PumpId = pump.PumpId, FuelType = FuelType.Diesel, LitresPumped = 1, TotalAmount = 1}, pump.CurrentTransaction);
        }

        [TestMethod]
        public void Activate_WithActiveFuelPriceServer_GetsFuelPrices()
        {
            var customerGenerator = new FakeCustomerGenerator();
            var server = new FakeActivationServer();
            var priceServer = new FakePriceServer();
            var transactionServer = new FakeTransactionServer();
            var pump = new Pump(server, customerGenerator, priceServer, transactionServer);

            customerGenerator.InvokeCustomerReady(new CustomerReadyEventArgs(FuelType.Hydrogen));
            pump.Activate();
            customerGenerator.InvokePumpProgress(new PumpProgressEventArgs(1));

            var expectedTransaction = new Transaction()
            {
                PumpId = pump.PumpId,
                FuelType = FuelType.Hydrogen,
                LitresPumped = 1,
                TotalAmount = priceServer.GetFuelPrices()[FuelType.Hydrogen],
            };
            Assert.AreEqual(expectedTransaction, pump.CurrentTransaction);
        }

        [TestMethod]
        public void PumpProgress_CalledTwice_IncrementsCurrentTransactionTwice()
        {
            var customerGenerator = new FakeCustomerGenerator();
            var server = new FakeActivationServer();
            var priceServer = new FakePriceServer();
            var transactionServer = new FakeTransactionServer();
            var pump = new Pump(server, customerGenerator, priceServer, transactionServer);

            customerGenerator.InvokeCustomerReady(new CustomerReadyEventArgs(FuelType.Unleaded));
            pump.Activate();
            customerGenerator.InvokePumpProgress(new PumpProgressEventArgs(1));
            customerGenerator.InvokePumpProgress(new PumpProgressEventArgs(2));

            var expectedTransaction = new Transaction()
            {
                PumpId = pump.PumpId,
                FuelType = FuelType.Unleaded,
                LitresPumped = 3,
                TotalAmount = 9,
            };
            Assert.AreEqual(expectedTransaction, pump.CurrentTransaction);
        }

        [TestMethod]
        public void PumpFinished_AfterPumpingFinished_MakesPumpGoInactiveAgain()
        {
            var customerGenerator = new FakeCustomerGenerator();
            var pump = InitializePumpWithFakes(customerGenerator: customerGenerator);

            customerGenerator.InvokeCustomerReady(new CustomerReadyEventArgs(FuelType.Diesel));
            pump.Activate();
            customerGenerator.InvokePumpProgress(new PumpProgressEventArgs(1));
            customerGenerator.InvokePumpingFinished(null);

            Assert.IsFalse(pump.IsActive, "Pump should not be reporting that it is still active after pumping has been finished.");
        }

        [TestMethod]
        public void PumpProgress_WithValidTransaction_SendsTransactionToServer()
        {
            var customerGenerator = new FakeCustomerGenerator();
            var transactionServer = new FakeTransactionServer();
            var pump = InitializePumpWithFakes(
                customerGenerator: customerGenerator,
                transactionServer: transactionServer);

            customerGenerator.InvokeCustomerReady(new CustomerReadyEventArgs(FuelType.Unleaded));
            pump.Activate();
            customerGenerator.InvokePumpProgress(new PumpProgressEventArgs(1));

            var expectedTransaction = new Transaction()
            {
                PumpId = pump.PumpId,
                FuelType = FuelType.Unleaded,
                LitresPumped = 1,
                TotalAmount = 3,
            };
            Assert.AreEqual(expectedTransaction, transactionServer.LastTransaction);
        }

        [TestMethod]
        public void PumpingFinished_AfterPumping_SendsFinishedMessageToTheServer()
        {
            var customerGenerator = new FakeCustomerGenerator();
            var activationServer = new FakeActivationServer();
            var pump = InitializePumpWithFakes(customerGenerator: customerGenerator, activationServer: activationServer);

            customerGenerator.InvokeCustomerReady(new CustomerReadyEventArgs(FuelType.Diesel));
            pump.Activate();
            customerGenerator.InvokePumpProgress(new PumpProgressEventArgs(1));
            customerGenerator.InvokePumpingFinished(new EventArgs());

            Assert.IsTrue(activationServer.PumpingFinished);
        }

        private static Pump InitializePumpWithFakes(
            IPumpActivationServer activationServer = null,
            ICustomerGenerator customerGenerator = null,
            IFuelPricesServer pricesServer = null,
            ITransactionServer transactionServer = null)
        {
            var fakeActivationServer = activationServer ?? new FakeActivationServer();
            var fakeCustomerGenerator = customerGenerator ?? new FakeCustomerGenerator();
            var fakePriceServer = pricesServer ?? new FakePriceServer();
            var fakeTransactionServer = transactionServer ?? new FakeTransactionServer();

            return new Pump(fakeActivationServer, fakeCustomerGenerator, fakePriceServer, fakeTransactionServer);
        }

    }
}
