using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetrofexSystem.PosTerminals
{
    public class PumpFactory
    {
        private readonly ICollection<Pump> _pumps;

        public PumpFactory()
        {
            this._pumps = new List<Pump>();
        }

        public Pump HandleActivationRequest(string pumpId)
        {
            var pump = this._pumps.FirstOrDefault(x => x.Id == pumpId);
            if (pump != null)
            {
                return pump;
            }

            var addedPump = new Pump(pumpId);
            this._pumps.Add(addedPump);
            return addedPump;
        }
    }
}
