using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetrofexSystem.PosTerminals
{
    public class PosTerminal
    {
        // TODO: Hack. This should probably be taken from a central service.
        private static readonly Dictionary<Guid, bool> PumpActivationsById = new Dictionary<Guid, bool>();

        public PosTerminal()
        {
        }

        public void ActivatePump(Guid pumpId)
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

        public bool PumpIsActive(Guid pumpId)
        {
            return PumpActivationsById.ContainsKey(pumpId) && PumpActivationsById[pumpId];
        }
    }
}
