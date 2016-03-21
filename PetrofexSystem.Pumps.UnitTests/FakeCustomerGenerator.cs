using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PumpLibrary;

namespace PetrofexSystem.Pumps.UnitTests
{
    class FakeCustomerGenerator : ICustomerGenerator
    {
        public event CustomerGenerator.CustomerReadyHandler CustomerReady;
        public event CustomerGenerator.PumpProgressHandler PumpProgress;
        public event CustomerGenerator.PumpingFinishedHandler PumpingFinished;
        public void ActivatePump()
        {
        }

        public void InvokeCustomerReady(CustomerReadyEventArgs eventArgs)
        {
            this.CustomerReady(this, eventArgs);
        }


    }
}
