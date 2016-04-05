using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PetrofexSystem.Server
{
    public class Server : IPumpActivationServer, INotifyPumpActivated
    {
        public void RequestActivation(string pumpId)
        {
            if (PumpActivationRequested != null)
            {
                this.PumpActivationRequested(pumpId);
            }
        }

        public void RequestDeactivation(string pumpId)
        {
            if (PumpDeactivationRequested != null)
            {
                this.PumpDeactivationRequested(pumpId);
            }
        }

        public event Action<string> PumpActivationRequested;
        public event Action<string> PumpDeactivationRequested;
        
    }
}
