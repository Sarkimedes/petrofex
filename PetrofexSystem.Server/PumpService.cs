using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using PetrofexSystem.Common;
using PetrofexSystem.PosTerminals;

namespace PetrofexSystem.Server
{
    public class PumpService
    {
        private readonly IPumpFactory _factory;

        public PumpService(IPumpFactory factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException("factory");
            }
            this._factory = factory;
        }

        public void HandleActivationRequest(string pumpId)
        {
            var pump = this._factory.GetPumpById(pumpId);
            if (pump != null)
            {
                pump.RequestActivation();
            }
        }

        public void HandlePumpProgress(Transaction transaction)
        {
            var pump = this._factory.GetPumpById(transaction.PumpId);
            if (pump != null)
            {
                pump.HandlePumpProgress(transaction);
            }
        }

        public void HandleDeactivationRequest(string pumpId)
        {
            var pump = this._factory.GetPumpById(pumpId);
            if (pump != null)
            {
                pump.Deactivate();
            }
        }
    }
}
