using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetrofexSystem.Server;

namespace PetrofexSystem.PosTerminals
{
    public class PumpFactory : IPumpFactory
    {
        private readonly ICollection<Pump> _pumps;
        private readonly IPaymentServer _paymentServer;

        public PumpFactory(IPaymentServer paymentServer)
        {
            this._pumps = new List<Pump>();
            this._paymentServer = paymentServer;
        }

        public Pump GetPumpById(string pumpId)
        {
            var pump = this._pumps.FirstOrDefault(x => x.Id == pumpId);
            if (pump != null)
            {
                return pump;
            }

            var addedPump = new Pump(pumpId, this._paymentServer);
            this._pumps.Add(addedPump);
            return addedPump;
        }
    }
}
