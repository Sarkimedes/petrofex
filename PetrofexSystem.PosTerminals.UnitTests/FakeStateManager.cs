using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetrofexSystem.PosTerminals.UnitTests
{
    class FakeStateManager : IStateManager
    {
        public PumpState CurrentState { get; private set; }
        public void SetState(PumpState newState)
        {
            this.CurrentState = newState;
        }
    }
}
