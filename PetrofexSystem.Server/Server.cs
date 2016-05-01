using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using PetrofexSystem.PosTerminals;

namespace PetrofexSystem.Server
{
    public class Server : IPumpActivationServer
    {
        private readonly IPumpFactory _factory;

        public Server(IPumpFactory factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException("factory");
            }
            this._factory = factory;
        }

        public void RequestActivation(string pumpId)
        {
            var pump = this._factory.GetPumpById(pumpId);
            if (pump != null)
            {
                pump.Activate();
            }
        }

        public void RequestDeactivation(string pumpId)
        {
            var pump = this._factory.GetPumpById(pumpId);
            if (pump != null)
            {
                pump.Deactivate();
            }
        }
    }
}
