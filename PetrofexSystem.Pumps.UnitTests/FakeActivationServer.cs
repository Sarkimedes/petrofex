using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetrofexSystem.Server;

namespace PetrofexSystem.Pumps.UnitTests
{
    class FakeActivationServer : IPumpActivationServer
    {
        public FakeActivationServer()
        {
            this.ActivationRequested = false;
        }

        public bool ActivationRequested { get; set; }
        public bool PumpingFinished { get; set; }

        public void RequestActivation(string pumpId)
        {
            this.ActivationRequested = true;
        }

        public void RequestDeactivation(string pumpId)
        {
            this.PumpingFinished = true;
        }
    }
}
