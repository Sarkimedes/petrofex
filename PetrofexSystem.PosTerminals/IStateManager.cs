using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetrofexSystem.PosTerminals
{
    public interface IStateManager
    {
        PumpState CurrentState { get; }
        void SetState(PumpState newState);
    }
}
