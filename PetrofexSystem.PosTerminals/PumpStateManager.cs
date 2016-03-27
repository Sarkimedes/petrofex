using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetrofexSystem.PosTerminals
{
    class PumpStateManager
    {
        public PumpState CurrentState { get; private set; }

        public void SetCurrentPumpState(PumpState newState)
        {
            this.CurrentState = newState;
        }
    }
}
