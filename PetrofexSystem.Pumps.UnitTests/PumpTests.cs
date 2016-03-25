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
            var server = new FakeActivationServer();
            var fuelPricesServer = new FakePriceServer();
            var transactionServer = new FakeTransactionServer();
            var pump = new Pump(server, customerGenerator, fuelPricesServer, transactionServer);

            customerGenerator.InvokeCustomerReady(new CustomerReadyEventArgs(FuelType.Diesel));

            Assert.IsTrue(server.ActivationRequested);
        }

        [TestMethod]
        public void Pump_CustomerReady_SetsNextTransactionToSuppliedFuelType()
        {
            var customerGenerator = new FakeCustomerGenerator();
            var server = new FakeActivationServer();
            var fuelPricesServer = new FakePriceServer();
            var transactionServer = new FakeTransactionServer();
            var pump = new Pump(server, customerGenerator, fuelPricesServer, transactionServer);

            customerGenerator.InvokeCustomerReady(new CustomerReadyEventArgs(FuelType.Diesel));

            Assert.AreEqual(pump.NextTransaction.FuelType, FuelType.Diesel);
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
        public void Activate_WithValidTransaction_SetsCurrentTransactionToNextTransaction()
        {
            var customerGenerator = new FakeCustomerGenerator();
            var server = new FakeActivationServer();
            var fuelPricesServer = new FakePriceServer();
            var transactionServer = new FakeTransactionServer();
            var pump = new Pump(server, customerGenerator, fuelPricesServer, transactionServer);

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

            Assert.AreEqual(new FuelTransaction() {FuelType = FuelType.Diesel, Total = 1}, pump.CurrentTransaction);
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

            var expectedTransaction = new FuelTransaction()
            {
                FuelType = FuelType.Hydrogen,
                Total = priceServer.GetFuelPrices()[FuelType.Hydrogen]
            };
            Assert.AreEqual(
                expectedTransaction,
                pump.CurrentTransaction,
                string.Format("Fuel type: {0} | Total: {1} did not match Fuel type: {2} | Total {3}",
                expectedTransaction.FuelType.ToString(),
                expectedTransaction.Total,
                pump.CurrentTransaction.FuelType.ToString(),
                pump.CurrentTransaction.Total
                ));
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

            var expectedTransaction = new FuelTransaction()
            {
                FuelType = FuelType.Unleaded,
                Total = 9
            };
            Assert.AreEqual(expectedTransaction, pump.CurrentTransaction);
        }

        [TestMethod]
        public void PumpFinished_AfterPumpingFinished_SendsCurrentTransactionToServer()
        {
            var customerGenerator = new FakeCustomerGenerator();
            var server = new FakeActivationServer();
            var priceServer = new FakePriceServer();
            var transactionServer = new FakeTransactionServer();
            var pump = new Pump(server, customerGenerator, priceServer, transactionServer);

            customerGenerator.InvokeCustomerReady(new CustomerReadyEventArgs(FuelType.Diesel));
            pump.Activate();
            customerGenerator.InvokePumpProgress(new PumpProgressEventArgs(1));
            customerGenerator.InvokePumpingFinished(null);

            var expectedTransaction = new FuelTransaction()
            {
                FuelType = FuelType.Diesel,
                Total = 1
            };
            Assert.AreEqual(expectedTransaction, transactionServer.LastTransaction);
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

        private Pump InitializePumpWithFakes(
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
