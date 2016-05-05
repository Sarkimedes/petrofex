using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetrofexSystem.Common;
using PetrofexSystem.Server;

namespace PetrofexSystem.PosTerminals
{
    /// <summary>
    /// Models a pump on the server side. Each 
    /// </summary>
    public class Pump
    {
        private readonly IPaymentServer _paymentServer;
        private PosTerminalService _posTerminalService;

        public Pump(string id, IPaymentServer paymentServer, IPumpFactory pumpFactory)
        {
            this.Id = id;
            this._paymentServer = paymentServer;
            this._posTerminalService = new PosTerminalService(pumpFactory);
        }

        public string Id { get; private set; }

        public Transaction CurrentTransaction { get; private set; }

        public void RequestActivation()
        {
            this._posTerminalService.HandleActivationRequest(this.Activate);
        }

        public void Activate()
        {
        }

        public void HandlePumpProgress(Transaction transaction)
        {
            this.CurrentTransaction = transaction;
            this._posTerminalService.HandlePumpProgress(transaction);            
        }

        public void Deactivate()
        {
            this.TransactionPaid = false;
            this._posTerminalService.HandlePaymentAwaiting(this.CurrentTransaction);
            
        }

        public void PayCurrentTransaction()
        {
            this._paymentServer.SendForProcessing(this.CurrentTransaction, this.HandlePaymentAcknowledged);            
        }

        public void HandlePaymentAcknowledged()
        {
            this.CurrentTransaction = new Transaction()
            {
                PumpId = this.CurrentTransaction.PumpId,
                FuelType = this.CurrentTransaction.FuelType,
                LitresPumped = this.CurrentTransaction.LitresPumped,
                TotalAmount = this.CurrentTransaction.TotalAmount,
            };
            this.TransactionPaid = true;            
        }

        public bool TransactionPaid { get; set; }
    }
}
