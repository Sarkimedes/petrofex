using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using PumpLibrary;

namespace PetrofexSystem.PosTerminals
{
    public class PumpManager
    {
        private readonly IDictionary<string, PumpStatus> _pumpStatuses;
        private readonly IDictionary<string, Transaction> _lastTransactionsByPumpId;

        public PumpManager()
        {
            this._pumpStatuses = new Dictionary<string, PumpStatus>();
            this._lastTransactionsByPumpId = new Dictionary<string, Transaction>();
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

            if (this._lastTransactionsByPumpId.ContainsKey(pumpId))
            {
                this._lastTransactionsByPumpId[pumpId] = new Transaction()
                {
                    FuelType = fuelType,
                    LitresPumped = litresPumped,
                    TotalAmount = totalPaid,
                    IsPaid = false
                };
            }
            else
            {
                this._lastTransactionsByPumpId.Add(pumpId, new Transaction()
                {
                    FuelType = fuelType,
                    LitresPumped = litresPumped,
                    TotalAmount = totalPaid,
                    IsPaid = false
                });
            }
        }

        public void HandleDeactivationRequest(string pumpId)
        {
            if (this._pumpStatuses.ContainsKey(pumpId))
            {
                this._pumpStatuses[pumpId] = PumpStatus.AwaitingPayment;
            }
        }

        public Transaction GetLatestTransaction(string pumpId)
        {
            return this._lastTransactionsByPumpId[pumpId];
        }
    }
}
