using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetrofexSystem.PosTerminals
{
    public class PosTerminalService
    {
        private IPumpFactory _pumpFactory;

        public PosTerminalService(IPumpFactory factory)
        {
            this._pumpFactory = factory;
        }

        public void HandlePumpActivationRequest(string id)
        {
            var pump = this._pumpFactory.GetPumpById(id);
            pump.Activate();
        }

        public void HandlePumpDeactivationRequest(string id)
        {
            this._pumpFactory.GetPumpById(id).Deactivate();
        }
    }
}
