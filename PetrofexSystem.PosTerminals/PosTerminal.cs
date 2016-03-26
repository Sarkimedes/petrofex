using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetrofexSystem.PosTerminals
{
    public class PosTerminal
    {
        private readonly PumpManager _pumpManager;

        public PosTerminal(PumpManager pumpManager)
        {
            this._pumpManager = pumpManager;
        }

        public void ActivatePump(string pumpId)
        {
            this._pumpManager.ActivatePump(pumpId);
        }

        public PumpStatus GetPumpStatus(string pumpId)
        {
            return this._pumpManager.GetPumpStatus(pumpId);
        }
    }
}
