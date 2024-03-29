﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetrofexSystem.Common;
using PetrofexSystem.Server;

namespace PetrofexSystem.PosTerminals
{
    /// <summary>
    /// Models a pump as POS terminals see it.
    /// </summary>
    public class Pump
    {
        private readonly IStateManager _stateManager;
        private readonly IPaymentServer _paymentServer;

        public Pump(string id, IPaymentServer paymentServer, IStateManager stateManager)
        {
            this.Id = id;
            this._paymentServer = paymentServer;
            this._stateManager = stateManager;
            this._stateManager.SetState(PumpState.CustomerWaiting);
        }

        public string Id { get; private set; }

        public PumpState CurrentState
        {
            get
            {
                return this._stateManager.CurrentState;
            }
        }

        public Transaction CurrentTransaction { get; private set; }

        public void Activate()
        {
            this._stateManager.SetState(PumpState.ActivationPending);
        }

        public void HandlePumpProgress(Transaction transaction)
        {
            this.CurrentTransaction = transaction;
            this._stateManager.SetState(PumpState.Active);
        }

        public void Deactivate()
        {
            this.TransactionPaid = false;
            this._stateManager.SetState(PumpState.AwaitingPayment);
        }

        public void PayCurrentTransaction()
        {
            this._paymentServer.SendForProcessing(this.CurrentTransaction);
            this._stateManager.SetState(PumpState.PaymentMade);
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
            this._stateManager.SetState(PumpState.Inactive);
        }

        public bool TransactionPaid { get; set; }
    }
}
