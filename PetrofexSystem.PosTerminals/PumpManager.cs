using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using PumpLibrary;

namespace PetrofexSystem.PosTerminals
{
    public class PumpManager
    {
        private readonly IDictionary<string, PumpStatus> _pumpStatuses;
        private readonly ICollection<Common.Transaction> _transactions;

        public PumpManager()
        {
            this._pumpStatuses = new Dictionary<string, PumpStatus>();
            this._transactions = new List<Common.Transaction>();
        }

        public void HandleActivationRequest(string pumpId)
        {
            if (this._pumpStatuses.ContainsKey(pumpId))
            {
                var currentStatus = GetPumpStatus(pumpId);
                if (currentStatus == PumpStatus.Inactive)
                {
                    this._pumpStatuses[pumpId] = PumpStatus.CustomerWaiting;
                }
            }
            else
            {
                this._pumpStatuses.Add(pumpId, PumpStatus.CustomerWaiting);
            }
        }

        public void ActivatePump(string pumpId)
        {
            var currentPumpStatus = this.GetPumpStatus(pumpId);
            if (currentPumpStatus.Equals(PumpStatus.CustomerWaiting))
            {
                this._pumpStatuses[pumpId] = PumpStatus.ActivationPending;
            }
        }

        public PumpStatus GetPumpStatus(string pumpId)
        {
            return this._pumpStatuses.ContainsKey(pumpId) ? this._pumpStatuses[pumpId] : PumpStatus.Error;
        }

        public void HandlePumpProgress(string pumpId, FuelType fuelType, double litresPumped, double totalPaid)
        {
            if (this._pumpStatuses.ContainsKey(pumpId))
            {
                this._pumpStatuses[pumpId] = PumpStatus.Active;
            }
            else
            {
                throw new InvalidOperationException(string.Format("Cannot handle progress on non-existent pump with ID {0}", pumpId));
            }

            this.UpdateLastTransactions(pumpId, fuelType, litresPumped, totalPaid);
        }

        private void UpdateLastTransactions(string pumpId, FuelType fuelType, double litresPumped, double totalPaid)
        {
            var existingTransaction = this.GetLatestTransaction(pumpId);
            if (!existingTransaction.Equals(default(Common.Transaction)))
            {
                this._transactions.Remove(existingTransaction);
            }
            var newTransaction = new Common.Transaction()
            {
                PumpId = pumpId,
                FuelType = fuelType,
                LitresPumped = litresPumped,
                TotalAmount = totalPaid,
                IsPaid = false
            };
            this._transactions.Add(newTransaction);
        }

        public void HandleDeactivationRequest(string pumpId)
        {
            if (this._pumpStatuses.ContainsKey(pumpId))
            {
                this._pumpStatuses[pumpId] = PumpStatus.AwaitingPayment;
            }
        }

        public Common.Transaction GetLatestTransaction(string pumpId)
        {
            return this._transactions.FirstOrDefault(x => x.PumpId == pumpId);
        }
    }
}
