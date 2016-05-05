using System.Collections.Generic;
using System.Linq;
using PetrofexSystem.PosTerminals;

namespace PetrofexSystem.Server
{
    public class PumpFactory : IPumpFactory
    {
        private readonly ICollection<Pump> _pumps;
        private readonly IPaymentServer _paymentServer;
        private readonly IStateManager _stateManager;



        public PumpFactory(IPaymentServer paymentServer, IStateManager stateManager)
        {
            this._pumps = new List<Pump>();
            this._paymentServer = paymentServer;
            this._stateManager = stateManager;
        }

        public Pump GetPumpById(string pumpId)
        {
            var pump = this._pumps.FirstOrDefault(x => x.Id == pumpId);
            if (pump != null)
            {
                return pump;
            }

            var addedPump = new Pump(pumpId, this._paymentServer, this._stateManager, this);
            this._pumps.Add(addedPump);
            return addedPump;
        }
    }
}
