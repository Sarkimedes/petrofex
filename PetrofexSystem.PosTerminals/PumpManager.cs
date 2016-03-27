using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using PetrofexSystem.Common;
using PumpLibrary;

namespace PetrofexSystem.PosTerminals
{
    public class PumpManager
    {
        private readonly IDictionary<string, PumpState> _pumpStates;
        private readonly ICollection<Transaction> _transactions;

        public PumpManager()
        {
            this._pumpStates = new Dictionary<string, PumpState>();
            this._transactions = new List<Transaction>();
        }

        public void HandleActivationRequest(string pumpId)
        {
            if (this._pumpStates.ContainsKey(pumpId))
            {
                var currentStatus = GetPumpStatus(pumpId);
                if (currentStatus == PumpState.Inactive)
                {
                    this._pumpStates[pumpId] = PumpState.CustomerWaiting;
                }
            }
            else
            {
                this._pumpStates.Add(pumpId, PumpState.CustomerWaiting);
            }
        }

        public void ActivatePump(string pumpId)
        {
            var currentPumpStatus = this.GetPumpStatus(pumpId);
            if (currentPumpStatus.Equals(PumpState.CustomerWaiting))
            {
                this._pumpStates[pumpId] = PumpState.ActivationPending;
            }
        }

        public PumpState GetPumpStatus(string pumpId)
        {
            return this._pumpStates.ContainsKey(pumpId) ? this._pumpStates[pumpId] : PumpState.Error;
        }

        public void HandlePumpProgress(string pumpId, FuelType fuelType, double litresPumped, double totalPaid)
        {
            if (this._pumpStates.ContainsKey(pumpId))
            {
                this._pumpStates[pumpId] = PumpState.Active;
            }
            else
            {
                throw new InvalidOperationException(string.Format("Cannot handle progress on non-existent pump with ID {0}", pumpId));
            }

            var newTransaction = new Transaction
            {
                PumpId = pumpId,
                FuelType = fuelType,
                LitresPumped = litresPumped,
                TotalAmount = totalPaid,
                IsPaid = false
            };
            this.UpdateLastTransactions(newTransaction);
        }

        private void UpdateLastTransactions(Transaction transaction)
        {
            var existingTransaction = this.GetLatestTransaction(transaction.PumpId);
            if (!existingTransaction.Equals(default(Transaction)))
            {
                this._transactions.Remove(existingTransaction);
            }
            this._transactions.Add(transaction);
        }

        public void HandleDeactivationRequest(string pumpId)
        {
            this.UpdateState(pumpId, PumpState.AwaitingPayment);
        }

        public Transaction GetLatestTransaction(string pumpId)
        {
            return this._transactions.FirstOrDefault(x => x.PumpId == pumpId);
        }

        public void SubmitPayment(string pumpId)
        {
            this.UpdateState(pumpId, PumpState.PaymentMade);
        }

        private void UpdateState(string pumpId, PumpState newState)
        {
            if (this._pumpStates.ContainsKey(pumpId))
            {
                this._pumpStates[pumpId] = newState;
            }
        }

        public void ReceivePaymentAcknowledged(string pumpId)
        {
            this.UpdateState(pumpId, PumpState.Inactive);
        }
    }
}
