using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using PetrofexSystem.Common;
using PetrofexSystem.Messaging;
using PetrofexSystem.Server;
using PumpLibrary;

namespace PetrofexSystem
{
    public class Pump
    {
        private readonly string _pumpId;
        private readonly IPumpActivationServer _pumpActivationServer;
        private readonly ICustomerGenerator _customerGenerator;
        private readonly IFuelPricesServer _fuelPricesServer;
        private readonly ITransactionServer _transactionServer;
        private IDictionary<FuelType, double> _fuelPrices;
       

        public string PumpId { get { return this._pumpId; } }

        public Transaction CurrentTransaction { get; private set; }

        public Pump(
            IPumpActivationServer pumpActivationServer,
            ICustomerGenerator customerGenerator,
            IFuelPricesServer fuelPricesServer,
            ITransactionServer transactionServer, 
            IMessagingClient client, 
            string id)
        {
            this._pumpActivationServer = pumpActivationServer;
            this._customerGenerator = customerGenerator;
            this._fuelPricesServer = fuelPricesServer;
            this._transactionServer = transactionServer;

            this._pumpId = id;
            this._customerGenerator.CustomerReady += CustomerGeneratorOnCustomerReady;
            this._customerGenerator.PumpProgress += CustomerGeneratorOnPumpProgress;
            this._customerGenerator.PumpingFinished += CustomerGeneratorOnPumpingFinished;

            client.Connect(message => { });
        }

        public void Activate()
        {
            this._fuelPrices = this._fuelPricesServer.GetFuelPrices();
            this._customerGenerator.ActivatePump();
            this.IsActive = true;
        }

        public bool IsActive { get; private set; }

        private void CustomerGeneratorOnCustomerReady(
            object sender,
            CustomerReadyEventArgs customerReadyEventArgs)
        {
            if (customerReadyEventArgs == null)
            {
                throw new ArgumentNullException("customerReadyEventArgs");
            }
            
            this._pumpActivationServer.RequestActivation(this.PumpId, this.Activate);
            this.CurrentTransaction = new Transaction()
            {
                PumpId = this.PumpId,
                FuelType = customerReadyEventArgs.SelectedFuel,
                LitresPumped = 0,
                TotalAmount = 0,
            };
        }

        private void CustomerGeneratorOnPumpProgress(
            object sender,
            PumpProgressEventArgs pumpProgressEventArgs)
        {
            if (pumpProgressEventArgs == null)
            {
                throw new ArgumentNullException("pumpProgressEventArgs");
            }

            var increment =
                this._fuelPrices[CurrentTransaction.FuelType] *
                pumpProgressEventArgs.LitresPumped;

            this.CurrentTransaction = new Transaction()
            {
                PumpId = this.PumpId,
                FuelType = CurrentTransaction.FuelType,
                LitresPumped = this.CurrentTransaction.LitresPumped + pumpProgressEventArgs.LitresPumped,
                TotalAmount = this.CurrentTransaction.TotalAmount + increment
            };

            this._transactionServer.SendFuelTransaction(this.CurrentTransaction);
        }

        private void CustomerGeneratorOnPumpingFinished(object sender, EventArgs eventArgs)
        {
            this._pumpActivationServer.RequestDeactivation(this.PumpId, () => this.IsActive = false);
        }
    }
}