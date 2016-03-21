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

        public string PumpId { get { return this._pumpId; } }

        public Pump(IPumpActivationServer pumpActivationServer, ICustomerGenerator customerGenerator)
        {
            this._pumpActivationServer = pumpActivationServer;
            this._customerGenerator = customerGenerator;
            this._pumpId = Guid.NewGuid().ToString();
            this._customerGenerator.CustomerReady += CustomerGeneratorOnCustomerReady;
        }

        internal FuelTransaction NextTransaction { get; private set; }

        private void CustomerGeneratorOnCustomerReady(object sender, CustomerReadyEventArgs customerReadyEventArgs)
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
    }
}
