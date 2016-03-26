using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetrofexSystem.PosTerminals
{
    public class PosTerminal
    {
        // TODO: Hack. This should probably be taken from a central service.
        private static readonly Dictionary<string, bool> PumpActivationsById = new Dictionary<string, bool>();
        private readonly PumpManager _pumpManager;

        public PosTerminal()
        { }

        public PosTerminal(PumpManager pumpManager)
        {
            this._pumpManager = pumpManager;
        }

        public void ActivatePump(string pumpId)
        {
            if (PumpActivationsById.ContainsKey(pumpId))
            {
                PumpActivationsById[pumpId] = true;
            }
            else
            {
                PumpActivationsById.Add(pumpId, true);
            }
        }

        public bool PumpIsActive(string pumpId)
        {
            return PumpActivationsById.ContainsKey(pumpId) && PumpActivationsById[pumpId];
        }

        public PumpStatus GetPumpStatus(string pumpId)
        {
            return this._pumpManager.GetPumpStatus(pumpId);
        }
    }
}
