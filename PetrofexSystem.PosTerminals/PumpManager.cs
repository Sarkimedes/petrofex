using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace PetrofexSystem.PosTerminals
{
    public class PumpManager
    {
        private readonly IDictionary<string, PumpStatus> _pumpStatuses;

        public PumpManager()
        {
            this._pumpStatuses = new Dictionary<string, PumpStatus>();
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

        public void HandlePumpProgress(string pumpId)
        {
            if (this._pumpStatuses.ContainsKey(pumpId))
            {
                this._pumpStatuses[pumpId] = PumpStatus.Active;
            }
            else
            {
                throw new InvalidOperationException(string.Format("Cannot handle progress on non-existent pump with ID {0}", pumpId));
            }
        }
    }
}
