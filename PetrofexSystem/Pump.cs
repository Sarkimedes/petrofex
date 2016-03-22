using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public FuelTransaction CurrentTransaction { get; private set; }

        internal FuelTransaction NextTransaction { get; private set; }

        public Pump(
            IPumpActivationServer pumpActivationServer,
            ICustomerGenerator customerGenerator,
            IFuelPricesServer fuelPricesServer,
            ITransactionServer transactionServer)
        {
            this._pumpActivationServer = pumpActivationServer;
            this._customerGenerator = customerGenerator;
            this._fuelPricesServer = fuelPricesServer;
            this._transactionServer = transactionServer;

            this._pumpId = Guid.NewGuid().ToString();
            this._customerGenerator.CustomerReady += CustomerGeneratorOnCustomerReady;
            this._customerGenerator.PumpProgress += CustomerGeneratorOnPumpProgress;
            this._customerGenerator.PumpingFinished += CustomerGeneratorOnPumpingFinished;
        }

        private void CustomerGeneratorOnPumpingFinished(object sender, EventArgs eventArgs)
        {
            this._transactionServer.SendFuelTransaction(this.CurrentTransaction);
        }

        private void CustomerGeneratorOnCustomerReady(
            object sender,
            CustomerReadyEventArgs customerReadyEventArgs)
        {
            if (customerReadyEventArgs == null)
            {
                throw new ArgumentNullException("customerReadyEventArgs");
            }

            this.NextTransaction = new FuelTransaction()
            {
                FuelType = customerReadyEventArgs.SelectedFuel,
                Total = 0
            };
            this._pumpActivationServer.RequestActivation(this.PumpId);
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
                this._fuelPrices[CurrentTransaction.FuelType]*
                pumpProgressEventArgs.LitresPumped;
            
            this.CurrentTransaction = new FuelTransaction()
            {
                FuelType = CurrentTransaction.FuelType,
                Total = CurrentTransaction.Total + increment
            };
        }

        public void Activate()
        {
            this._fuelPrices = this._fuelPricesServer.GetFuelPrices();
            this.CurrentTransaction = this.NextTransaction;
            this._customerGenerator.ActivatePump();
        }
    }
}
