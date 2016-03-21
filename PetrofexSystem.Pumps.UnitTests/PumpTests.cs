﻿using System;
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
            var fuelPricesServer = new FakePriceServer();
            var pump = new Pump(server, customerGenerator, fuelPricesServer);

            customerGenerator.InvokeCustomerReady(new CustomerReadyEventArgs(FuelType.Diesel));

            Assert.IsTrue(server.ActivationRequested);
        }

        [TestMethod]
        public void Pump_CustomerReady_SetsNextTransactionToSuppliedFuelType()
        {
            var customerGenerator = new FakeCustomerGenerator();
            var server = new FakeServer();
            var fuelPricesServer = new FakePriceServer();
            var pump = new Pump(server, customerGenerator, fuelPricesServer);

            customerGenerator.InvokeCustomerReady(new CustomerReadyEventArgs(FuelType.Diesel));

            Assert.AreEqual(pump.NextTransaction.FuelType, FuelType.Diesel);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CustomerReady_WithNoEventArgs_ThrowsArgumentNullException()
        {
            var customerGenerator = new FakeCustomerGenerator();
            var server = new FakeServer();
            var fuelPricesServer = new FakePriceServer();
            var pump = new Pump(server, customerGenerator, fuelPricesServer);

            customerGenerator.InvokeCustomerReady(null);
        }

        [TestMethod]
        public void Activate_WithValidTransaction_SetsCurrentTransactionToNextTransaction()
        {
            var customerGenerator = new FakeCustomerGenerator();
            var server = new FakeServer();
            var fuelPricesServer = new FakePriceServer();
            var pump = new Pump(server, customerGenerator, fuelPricesServer);

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
            var fuelPricesServer = new FakePriceServer();
            var pump = new Pump(server, customerGenerator, fuelPricesServer);

            customerGenerator.InvokeCustomerReady(new CustomerReadyEventArgs(FuelType.Diesel));
            pump.Activate();

            Assert.IsTrue(customerGenerator.IsPumpActive);
        }

        [TestMethod]
        public void PumpProgress_AfterActivation_IncrementsCurrentTransaction()
        {
            var customerGenerator = new FakeCustomerGenerator();
            var server = new FakeServer();
            var fuelPricesServer = new FakePriceServer();
            var pump = new Pump(server, customerGenerator, fuelPricesServer);

            customerGenerator.InvokeCustomerReady(new CustomerReadyEventArgs(FuelType.Diesel));
            pump.Activate();
            customerGenerator.InvokePumpProgress(new PumpProgressEventArgs(1));

            Assert.AreEqual(new FuelTransaction() {FuelType = FuelType.Diesel, Total = 1}, pump.CurrentTransaction);
        }

        [TestMethod]
        public void Activate_WithActiveFuelPriceServer_GetsFuelPrices()
        {
            var customerGenerator = new FakeCustomerGenerator();
            var server = new FakeServer();
            var priceServer = new FakePriceServer();
            var pump = new Pump(server, customerGenerator, priceServer);

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
    }
}
