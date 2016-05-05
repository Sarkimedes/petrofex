using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetrofexSystem.Server;

namespace PetrofexSystem.Pumps.TestApp
{
    class LocalActivationServer : IPumpActivationServer
    {
        private readonly List<Pump> _knownPumps;

        public LocalActivationServer()
        {
            this._knownPumps = new List<Pump>();
        }

        public void RegisterPump(Pump pump)
        {
            this._knownPumps.Add(pump);
        }

        public void RequestActivation(string pumpId, Action successCallback)
        {            
            Console.WriteLine("Requested activation for pump ID: {0}", pumpId);
            var matchingPumps = this._knownPumps.Where(x => string.Equals(x.PumpId, pumpId, StringComparison.InvariantCulture));
            foreach (var pump in matchingPumps)
            {
                Console.WriteLine("Activated pump with ID {0}", pump.PumpId);
                pump.Activate();
            }
        }

        public void RequestDeactivation(string pumpId, Action successCallback)
        {
            Console.WriteLine("Requested deactivation for pump ID: {0}", pumpId);
            var matchingPumps =
                this._knownPumps.Where(x => string.Equals(x.PumpId, pumpId, StringComparison.InvariantCulture));
            foreach (var pump in matchingPumps)
            {
                Console.WriteLine("Deactivated pump with ID {0}", pump.PumpId);
            }
        }
    }
}
