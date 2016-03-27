using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetrofexSystem.PosTerminals
{
    /// <summary>
    /// Models a pump as POS terminals see it.
    /// </summary>
    public class Pump
    {
        private readonly PumpStateManager _stateManager;

        public Pump(string id)
        {
            this.Id = id;

            this._stateManager = new PumpStateManager();
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

        public void Activate()
        {
            this._stateManager.SetState(PumpState.ActivationPending);
        }
    }
}
